using GwizdWebAPI.Database;
using GwizdWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GwizdWebAPI.Repositories;

public class DisappearedAnimalRepository
{
    private readonly DatabaseContext _context;

    public DisappearedAnimalRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<DisappearedAnimalEntity?>> GetAllDisappearedAnimalsAsync()
    {
        return (await _context.DisappearedAnimals.ToListAsync())!;
    }

    public async Task<DisappearedAnimalEntity?> GetDisappearedAnimalAsync(int animalImageId)
    {
        return await _context.DisappearedAnimals.FindAsync(animalImageId);
    }

    public async Task AddDisappearedAnimalAsync(DisappearedAnimalEntity? disappearedAnimal)
    {
        _context.DisappearedAnimals.Add(disappearedAnimal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDisappearedAnimalAsync(DisappearedAnimalEntity animalImage)
    {
        _context.Entry(animalImage).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDisappearedAnimalAsync(int animalImageId)
    {
        var disappearedAnimal = await _context.DisappearedAnimals.FindAsync(animalImageId);
        if (disappearedAnimal != null)
        {
            _context.DisappearedAnimals.Remove(disappearedAnimal);
            await _context.SaveChangesAsync();
        }
    }
}