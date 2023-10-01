using GwizdWebAPI.Database;
using GwizdWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GwizdWebAPI.Repositories;

public class AnimalImageRepository
{
    private readonly DatabaseContext _context;

    public AnimalImageRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<AnimalImageEntity?>> GetAllAnimalImagesAsync()
    {
        return await _context.AnimalImages.ToListAsync();
    }

    public async Task<List<AnimalImageEntity?>> GetAnimalImageByIdAsync(int animalImageId)
    {
        return await _context.AnimalImages
            .Where(image => (image.IsAnimalLost && image.DisappearedAnimalEntityDisappearedAnimalId == animalImageId) ||
                            (!image.IsAnimalLost && image.FoundedAnimalEntityFoundedAnimalId == animalImageId))
            .ToListAsync();    
    }

    public async Task AddAnimalImageAsync(AnimalImageEntity? animalImage)
    {
        _context.AnimalImages.Add(animalImage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAnimalImageAsync(AnimalImageEntity animalImage)
    {
        _context.Entry(animalImage).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAnimalImageAsync(int animalImageId)
    {
        var animalImage = await _context.AnimalImages.FindAsync(animalImageId);
        if (animalImage != null)
        {
            _context.AnimalImages.Remove(animalImage);
            await _context.SaveChangesAsync();
        }
    }
}