namespace GeoDataApi.Services;

public interface IAuthenticationService
{
    string CreateToken(string email);
}