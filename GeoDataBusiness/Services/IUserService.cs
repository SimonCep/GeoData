using GeoDataInfrastructure.Models;

namespace GeoDataBusiness.Services;

public interface IUserService
{
    Task CreateAsync(User user);
    Task<User?> GetAsync(string email);
}