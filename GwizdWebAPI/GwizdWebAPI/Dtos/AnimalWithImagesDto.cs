namespace GwizdWebAPI.Dtos;

public class AnimalWithImagesDto : AnimalDto
{
    public List<string> Images { get; set; }
}