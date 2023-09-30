namespace GwizdWebAPI.Entities;

public class FoundedAnimalEntity
{
    public int FoundedAnimalId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
    public string SpeciesName { get; set; }
    public int? ReporterId { get; set; }
    public UserEntity Reporter { get; set; }
    public List<AnimalImageEntity> Images { get; set; }
}