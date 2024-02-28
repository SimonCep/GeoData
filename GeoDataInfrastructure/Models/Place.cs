namespace GeoDataInfrastructure.Models;

public class Place
{
    public int Id { get; set; }
    public int Population { get; set; }
    public double Rating { get; set; }
    public string Hierarchy { get; set; } = string.Empty;
}