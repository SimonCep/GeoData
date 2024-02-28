namespace GeoDataInfrastructure.Models;

public class Location
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Altitude { get; set; }
    public int PlaceId { get; set; }
}