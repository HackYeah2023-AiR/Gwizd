namespace GwizdWebAPI.Dtos;

public class AnimalLocationPossibilityRequest
{
    public int SearchedAnimalId { get; set; }
    public int[] CurrentlyFoundAnimalIds { get; set; }
}