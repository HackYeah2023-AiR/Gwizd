using GwizdWebAPI.Dtos;
using GwizdWebAPI.Entities;
using GwizdWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<AnimalDto>> Post([FromBody] AnimalDto animal)
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
            return CreatedAtRoute("Get", new {id = animal.Id}, animal);
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