namespace GeoDataInfrastructure.Models;

public class Boundary
{
    public int Id { get; set; }
    public string GeoJson { get; set; } = string.Empty;
    public int PlaceId { get; set; }
}