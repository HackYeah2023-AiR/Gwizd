using GwizdWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GwizdWebAPI.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AnimalImageEntity> AnimalImages { get; set; }
    public DbSet<AnimalSuggestionEntity> AnimalSuggestions { get; set; }
    public DbSet<DisappearedAnimalEntity> DisappearedAnimals { get; set; }
    public DbSet<FoundedAnimalEntity> FoundedAnimals { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<AnimalImageEntity>()
            .HasKey(x => x.AnimalImageId);
        modelBuilder.Entity<AnimalSuggestionEntity>()
            .HasKey(x => x.AnimalSuggestionID);
        modelBuilder.Entity<DisappearedAnimalEntity>()
            .HasKey(x => x.DisappearedAnimalId);
        modelBuilder.Entity<FoundedAnimalEntity>()
            .HasKey(x => x.FoundedAnimalId);
    }
}