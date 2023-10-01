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
public class FoundedAnimalsController : ControllerBase
{
    private readonly FoundedAnimalRepository _foundedAnimalRepository;

    public FoundedAnimalsController(FoundedAnimalRepository foundedAnimalRepository)
    {
        _foundedAnimalRepository = foundedAnimalRepository;
    }

    [HttpGet("GetAllFounded")]
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetFounded()
    {
        try
        {
            var result = await _foundedAnimalRepository.GetAllFoundedAnimalsAsync();
            var dtos = result.Select(x => new AnimalDto()
            {
                AnimalName = x.SpeciesName,
                Date = x.Date,
                Id = x.FoundedAnimalId,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                RelatedUserId = x.ReporterId
            });
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
    
    [HttpGet("GetAllFoundedWithImages")]
    public async Task<ActionResult<IEnumerable<AnimalWithImagesDto>>> GetAllFoundedWithImages()
    {
        try
        {
            var result = await _foundedAnimalRepository.GetAllFoundedAnimalsAsync();
            var animalsWithImagesTasks = result.Select(async x =>
            {
                var images = await GetAnimalImages(x.FoundedAnimalId);
                return new AnimalWithImagesDto()
                {
                    AnimalName = x.SpeciesName,
                    Date = x.Date,
                    Id = x.FoundedAnimalId,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    RelatedUserId = x.ReporterId,
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
    public async Task<ActionResult<AnimalDto>> GetFounded(int id)
    {
        try
        {
            var animal = await _foundedAnimalRepository.GetFoundedAnimalByIdAsync(id);
            if (animal == null)
                return NotFound();

            return Ok(animal);
        }        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }

    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AnimalDto animal)
    {
        try
        {
            var entity = new FoundedAnimalEntity
            {
                Date = animal.Date,
                FoundedAnimalId = animal.Id,
                Latitude = animal.Latitude,
                Longitude = animal.Longitude,
                ReporterId = animal.RelatedUserId,
                SpeciesName = animal.AnimalName
            };
            await _foundedAnimalRepository.AddFoundedAnimalAsync(entity);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    // [HttpPut("{id}")]
    // public IActionResult Put(int id, [FromBody] AnimalDto updatedAnimal)
    // {
    //     var existingAnimal = _animals.FirstOrDefault(a => a.FoundedAnimalId == id);
    //     if (existingAnimal == null)
    //         return NotFound();
    //
    //     existingAnimal.Latitude = updatedAnimal.Latitude;
    //     existingAnimal.Longitude = updatedAnimal.Longitude;
    //     existingAnimal.Date = updatedAnimal.Date;
    //     existingAnimal.SpeciesName = updatedAnimal.AnimalName;
    //     existingAnimal.Reporter = updatedAnimal.RelatedUserId;
    //
    //     return NoContent();
    // }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _foundedAnimalRepository.DeleteFoundedAnimalAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
}