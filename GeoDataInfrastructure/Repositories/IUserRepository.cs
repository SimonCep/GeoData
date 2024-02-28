using GeoDataInfrastructure.Models;

namespace GeoDataInfrastructure.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetAsync(string email);
}