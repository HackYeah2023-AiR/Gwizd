namespace GwizdWebAPI.Entities;

public class DisappearedAnimalEntity
{
    public int DisappearedAnimalId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
    public string SpeciesName { get; set; }
    public int? OwnerId { get; set; }
    public UserEntity Owner { get; set; }
    public List<AnimalImageEntity> Images { get; set; }
}