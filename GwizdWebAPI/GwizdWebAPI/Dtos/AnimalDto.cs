using GwizdWebAPI.Entities;

namespace GwizdWebAPI.Dtos;

public class AnimalDto
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
    public string AnimalName { get; set; }
    public int? RelatedUserId { get; set; }
}