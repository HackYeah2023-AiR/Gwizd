namespace GwizdWebAPI.Entities;

public class AnimalSuggestionEntity
{
    public int AnimalSuggestionID { get; set; }
    public double Similarity { get; set; }
    public int? DisappearedAnimalId { get; set; }
    public DisappearedAnimalEntity DisappearedAnimal { get; set; }
    public int? FoundedAnimalId { get; set; }
    public FoundedAnimalEntity FoundedAnimal { get; set; }
}