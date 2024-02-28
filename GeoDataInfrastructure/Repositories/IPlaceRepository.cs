using GeoDataInfrastructure.Models;
using System.Data;
using System.Transactions;

namespace GeoDataInfrastructure.Repositories;

public interface IPlaceRepository
{
    Task<int> CreateAsync(Place place, IDbTransaction? transaction = null);
    Task<Place?> GetAsync(int id);
    Task<IEnumerable<Place>> GetAsync();
    Task UpdateAsync(int id, Place place);
    Task DeleteAsync(int id);
}