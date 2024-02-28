using GeoDataInfrastructure.Models;

namespace GeoDataBusiness.Services;

public interface ILocationService
{
    Task CreateAsync(Location location);
    Task<Location?> GetAsync(int id);
    Task<IEnumerable<Location>> GetAsync();
    Task UpdateAsync(int id, Location location);
    Task DeleteAsync(int id);
}