namespace GwizdWebAPI.Dtos;

public class FindSimilarAnimalsRequest
{
    public int SearchedAnimalId { get; set; }
    public int[] CurrentlyFoundAnimalIds { get; set; }
}