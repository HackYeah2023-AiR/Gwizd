using System.Text;
using GwizdWebAPI.Dtos;
using GwizdWebAPI.Entities;
using GwizdWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GwizdWebAPI.Controllers;


[AllowAnonymous]
[Route("[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly DisappearedAnimalRepository _disappearedAnimalRepository;
    private readonly FoundedAnimalRepository _foundedAnimalRepository;

    public AnimalsController(DisappearedAnimalRepository disappearedAnimalRepository, FoundedAnimalRepository foundedAnimalRepository)
    {
        _disappearedAnimalRepository = disappearedAnimalRepository;
        _foundedAnimalRepository = foundedAnimalRepository;
    }

    [HttpPost]
    public async Task<ActionResult<AnimalDto>> Post(AnimalCreationDto animalCreationDto)
    {
        if (!ValidateAnimalCreation(animalCreationDto))
        {
            return BadRequest("Payload was not valid");
        }
        try
        {
            if (animalCreationDto.Image != null)
            {
                var croppedImage = await CropImage(new AnimalImageDto
                {
                    Image = animalCreationDto.Image
                });
                if (!string.IsNullOrEmpty(croppedImage))
                {
                    animalCreationDto.Image = animalCreationDto.Image;
                }
                else
                {
                    return BadRequest("There is no animal on the picture");
                }
            }
            await CreateAnimalInDatabase(animalCreationDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }

        return Ok();
    }

    private bool ValidateAnimalCreation(AnimalCreationDto animalCreationDto)
    {
        if (animalCreationDto.Latitude is > 90 or < -90)
        {
            return false;
        }
        if (animalCreationDto.Longitude is > 90 or < -90)
        {
            return false;
        }
        if (animalCreationDto.Date > DateTime.Now)
        {
            return false;
        }
        return true;
    }


    private async Task CreateAnimalInDatabase(AnimalCreationDto animal)
    {
        if (!animal.IsLost)
        {
            var entity = new FoundedAnimalEntity
            {
                Date = animal.Date,
                Latitude = animal.Latitude,
                Longitude = animal.Longitude,
                ReporterId = 0,
                SpeciesName = animal.SpeciesName,
                Images = new List<AnimalImageEntity>
                {
                    new AnimalImageEntity()
                    {
                        ImageBlob = Encoding.UTF8.GetBytes(animal.Image)
                    }
                }
            };
            await _foundedAnimalRepository.AddFoundedAnimalAsync(entity);
        }
        else
        {
            var entity = new DisappearedAnimalEntity()
            {
                Date = animal.Date,
                Latitude = animal.Latitude,
                Longitude = animal.Longitude,
                OwnerId = 0,
                SpeciesName = animal.SpeciesName,
                Images = new List<AnimalImageEntity>
                {
                    new AnimalImageEntity()
                    {
                        ImageBlob = Encoding.UTF8.GetBytes(animal.Image)
                    }
                }
            };
            await _disappearedAnimalRepository.AddDisappearedAnimalAsync(entity);
        }
    }
    
    private async Task<string> CropImage(AnimalImageDto imageDto)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Constants.ExternalUrl);
        try
        {
            var request = new SelectImageRequest
            {
                WildAnimalImage = imageDto.Image
            };
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/select_image", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

}