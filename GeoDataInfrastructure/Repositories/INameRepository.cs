using GeoDataInfrastructure.Models;
using System.Data;

namespace GeoDataInfrastructure.Repositories;

public interface INameRepository
{
    Task CreateAsync(Name name, IDbTransaction? transaction = null);
    Task<IEnumerable<Name>> GetByPlaceIdAsync(int placeId);
    Task<Name?> GetAsync(string value);
    Task<Name?> GetAsync(int placeId, string locale);
    Task<IEnumerable<Name>> GetAsync();
    Task<IEnumerable<Name>> GetAsync(string value, string locale);
    Task UpdateAsync(int id, Name name);
    Task DeleteAsync(int id);
}