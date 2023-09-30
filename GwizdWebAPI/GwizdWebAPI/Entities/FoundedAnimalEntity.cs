namespace GwizdWebAPI.Entities;

public class FoundedAnimalEntity
{
    public int FoundedAnimalId { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public string SpeciesName { get; set; }
    public UserEntity Reporter { get; set; }
    public List<AnimalImageEntity> Images { get; set; }
}