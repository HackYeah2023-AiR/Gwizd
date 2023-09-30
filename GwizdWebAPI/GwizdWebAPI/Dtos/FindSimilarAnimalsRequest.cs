namespace GwizdWebAPI.Dtos;

public class FindSimilarAnimalsRequest
{
    public int DisappearedAnimalId { get; set; }
    public int[] FoundedAnimalIds { get; set; }
}