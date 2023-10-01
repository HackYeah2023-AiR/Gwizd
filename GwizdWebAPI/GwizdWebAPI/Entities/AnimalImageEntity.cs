namespace GwizdWebAPI.Entities;

public class AnimalImageEntity
{
    public int AnimalImageId { get; set; }
    public byte[] ImageBlob { get; set; }

    public int? DisappearedAnimalEntityDisappearedAnimalId { get; set; }
    public DisappearedAnimalEntity DisappearedAnimal { get; set; }

    public int? FoundedAnimalEntityFoundedAnimalId { get; set; }
    public FoundedAnimalEntity FoundedAnimal { get; set; }

    public bool IsAnimalLost { get; set; }
}