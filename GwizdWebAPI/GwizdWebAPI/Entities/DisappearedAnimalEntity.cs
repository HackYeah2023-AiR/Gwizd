namespace GwizdWebAPI.Entities;

public class DisappearedAnimalEntity
{
    public int DisappearedAnimalId { get; set; }
    public string DisapperanceLocation { get; set; }
    public DateTime DisappearanceDate { get; set; }
    public string SpeciesName { get; set; }
    public int? OwnerId { get; set; }
    public UserEntity Owner { get; set; }
    public List<AnimalImageEntity> Images { get; set; }
}