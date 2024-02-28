namespace GeoDataApi.Services;

public interface IHashService
{
    string ComputeSHA256(string text);
}