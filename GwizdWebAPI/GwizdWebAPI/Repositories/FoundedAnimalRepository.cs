using GwizdWebAPI.Database;
using GwizdWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GwizdWebAPI.Repositories;

public class FoundedAnimalRepository
{
    private readonly DatabaseContext _context;

    public FoundedAnimalRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<FoundedAnimalEntity?>> GetAllFoundedAnimalsAsync()
    {
        return (await _context.FoundedAnimals.ToListAsync())!;
    }

    public async Task<FoundedAnimalEntity?> GetFoundedAnimalByIdAsync(int animalImageId)
    {
        return await _context.FoundedAnimals.FindAsync(animalImageId);
    }

    public async Task AddFoundedAnimalAsync(FoundedAnimalEntity? foundedAnimal)
    {
        _context.FoundedAnimals.Add(foundedAnimal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFoundedAnimalAsync(FoundedAnimalEntity animalImage)
    {
        _context.Entry(animalImage).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFoundedAnimalAsync(int animalImageId)
    {
        var foundedAnimal = await _context.FoundedAnimals.FindAsync(animalImageId);
        if (foundedAnimal != null)
        {
            _context.FoundedAnimals.Remove(foundedAnimal);
            await _context.SaveChangesAsync();
        }
    }
}