using System.Text;
using GwizdWebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using GwizdWebAPI.Entities;
using GwizdWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace GwizdWebAPI.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly AnimalImageRepository _animalImageRepository;

        public ImagesController(AnimalImageRepository animalImageRepository)
        {
            _animalImageRepository = animalImageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimalImageEntity>>> Get()
        {
            try
            {
                var animalImages = await _animalImageRepository.GetAllAnimalImagesAsync();
                return Ok(animalImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{id}", Name = "GetImageById")]
        public async Task<ActionResult<AnimalImageEntity>> Get(int id)
        {
            try
            {
                var animalImage = await _animalImageRepository.GetAnimalImageByIdAsync(id);

                if (animalImage == null)
                    return NotFound();

                return Ok(animalImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AnimalImageDto animalImage)
        {
            var imageBlob = Encoding.UTF8.GetBytes(animalImage.Image);
            var imageEntity = new AnimalImageEntity
            {
                ImageBlob = imageBlob
            };
            try
            {
                await _animalImageRepository.AddAnimalImageAsync(imageEntity);
                return CreatedAtRoute("GetImageById", new { id = imageEntity.AnimalImageId }, animalImage);
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
                await _animalImageRepository.DeleteAnimalImageAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
