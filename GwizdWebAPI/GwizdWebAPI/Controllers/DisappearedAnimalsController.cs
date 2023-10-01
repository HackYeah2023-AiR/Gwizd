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
public class DisappearedAnimalsController : ControllerBase
{
    private readonly DisappearedAnimalRepository _disappearedAnimalRepository;

    public DisappearedAnimalsController(DisappearedAnimalRepository disappearedAnimalRepository)
    {
        _disappearedAnimalRepository = disappearedAnimalRepository;
    }

    [HttpGet("GetAllSortedDisappeared/{DisappearedAnimalId}")]
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAllDisappearedSorted(int DisappearedAnimalId)
    {
        try
        {
            await GetAnimalSuggestions(DisappearedAnimalId);
            var result = await _disappearedAnimalRepository.GetAllDisappearedAnimalsAsync();
            var dtos = result.Select(x => new AnimalDto()
            {
                AnimalName = x.SpeciesName,
                Date = x.Date,
                Id = x.DisappearedAnimalId,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                RelatedUserId = x.OwnerId
            });
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
    
    [HttpGet("GetAllDisappearedWithImages")]
    public async Task<ActionResult<IEnumerable<AnimalWithImagesDto>>> GetAllDisappearedWithImages()
    {
        try
        {
            var result = await _disappearedAnimalRepository.GetAllDisappearedAnimalsAsync();
            var animalsWithImagesTasks = result.Select(async x =>
            {
                var images = await GetAnimalImages(x.DisappearedAnimalId);
                return new AnimalWithImagesDto()
                {
                    AnimalName = x.SpeciesName,
                    Date = x.Date,
                    Id = x.DisappearedAnimalId,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    RelatedUserId = x.OwnerId,
                    Images = images.Select(image => Encoding.UTF8.GetString(image.ImageBlob)).ToList()
                };
            });

            var animalsWithImages = await Task.WhenAll(animalsWithImagesTasks);
            return Ok(animalsWithImages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
    
    
    private async Task GetAnimalSuggestions(int DisappearedAnimalId)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Constants.ExternalUrl);
        var request = new GetSuggestedAnimalsRequest
        {
            DisappearedAnimalId = DisappearedAnimalId
        };
        var jsonRequest = JsonConvert.SerializeObject(request);
        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/sort_suggested_animals", content);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);
        }
        else
        {
            Console.WriteLine("Failed to retrieve similarity points. Status code: " + response.StatusCode);
        }
    }
    
    private async Task<List<AnimalImageEntity>> GetAnimalImages(int animalId)
    {
        var HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri(Constants.HostUrl);
        var response = await HttpClient.GetAsync($"/Images/{animalId}");
        if (response.IsSuccessStatusCode)
        {
            var images = await response.Content.ReadAsStringAsync();
            var imagesEntities = JsonConvert.DeserializeObject<List<AnimalImageEntity>>(images);

            return imagesEntities;
        }

        return new List<AnimalImageEntity>();
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<AnimalDto>> Get(int id)
    {
        try
        {
            var animal = await _disappearedAnimalRepository.GetDisappearedAnimalAsync(id);
            if (animal == null)
                return NotFound();

            return Ok(animal);
        }        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _disappearedAnimalRepository.DeleteDisappearedAnimalAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
}