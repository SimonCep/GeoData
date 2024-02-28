using GeoDataInfrastructure.Models;

namespace GeoDataBusiness.Services;

public interface ITagService
{
    Task CreateAsync(Tag tag);
    Task<Tag?> GetAsync(int id);
    Task<IEnumerable<Tag>> GetAsync();
    Task<IEnumerable<Tag>> GetByPlaceIdAsync(int placeId);
    Task UpdateAsync(int id, Tag tag);
    Task DeleteAsync(int id);
}