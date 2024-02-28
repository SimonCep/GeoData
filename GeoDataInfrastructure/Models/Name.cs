namespace GeoDataInfrastructure.Models;

public class Name
{
    public int Id { get; set; }
    public string Locale { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int PlaceId { get; set; }
}