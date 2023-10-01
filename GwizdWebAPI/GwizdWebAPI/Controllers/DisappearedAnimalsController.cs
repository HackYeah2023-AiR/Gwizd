using GwizdWebAPI.Dtos;
using GwizdWebAPI.Entities;
using GwizdWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("GetAllDisappeared")]
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetDisappeared()
    {
        try
        {
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

    [HttpPost]
    public async Task<ActionResult<AnimalDto>> Post([FromBody] AnimalDto animal)
    {
        try
        {
            var entity = new DisappearedAnimalEntity
            {
                Date = animal.Date,
                DisappearedAnimalId = animal.Id,
                Latitude = animal.Latitude,
                Longitude = animal.Longitude,
                OwnerId = animal.RelatedUserId,
                SpeciesName = animal.AnimalName
            };
            await _disappearedAnimalRepository.AddDisappearedAnimalAsync(entity);
            return CreatedAtRoute("Get", new {id = animal.Id}, animal);
        }
        catch (Exception ex)
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