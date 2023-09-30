namespace GwizdWebAPI.Dtos;

public class FindSimilarAnimalsRequest
{
    public int SearchedAnimalId { get; set; }
    public int[] CurrentlyFoundAnimalsIds { get; set; }
}