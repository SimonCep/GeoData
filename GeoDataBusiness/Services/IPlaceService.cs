using GeoDataInfrastructure.Models;

namespace GeoDataBusiness.Services;

public interface IPlaceService
{
    Task CreateAsync(Place place);
    Task<Place?> GetAsync(int id);
    Task<IEnumerable<Place>> GetAsync();
    Task UpdateAsync(int id, Place place);
    Task DeleteAsync(int id);
}