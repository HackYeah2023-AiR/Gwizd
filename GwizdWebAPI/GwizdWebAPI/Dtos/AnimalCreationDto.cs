namespace GwizdWebAPI.Dtos;

public class AnimalCreationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
    public bool IsLost { get; set; }
    public string? Image { get; set; }
    public string? SpeciesName { get; set; }
}