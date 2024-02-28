namespace GeoDataInfrastructure.Models;

public class Tag
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int PlaceId { get; set; }
}