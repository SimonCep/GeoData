using GeoDataInfrastructure.Models;

namespace GeoDataBusiness.Services;

public interface INameService
{
    Task CreateAsync(Name name);
    Task<Name?> GetAsync(string value);
    Task<Name?> GetAsync(int placeId, string locale);
    Task<IEnumerable<Name>> GetAsync();
    Task<IEnumerable<Name>> GetAsync(string value, string locale);
    Task<IEnumerable<Name>> GetByPlaceIdAsync(int placeId);
    Task UpdateAsync(int id, Name name);
    Task DeleteAsync(int id);
}