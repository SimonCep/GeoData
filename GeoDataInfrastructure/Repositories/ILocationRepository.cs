using GeoDataInfrastructure.Models;
using System.Data;

namespace GeoDataInfrastructure.Repositories;

public interface ILocationRepository
{
    Task CreateAsync(Location location, IDbTransaction? transaction = null);
    Task<Location?> GetAsync(int id);
    Task<IEnumerable<Location>> GetAsync();
    Task UpdateAsync(int id, Location location);
    Task DeleteAsync(int id);
}