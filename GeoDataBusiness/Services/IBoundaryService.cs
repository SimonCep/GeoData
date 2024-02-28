using GeoDataInfrastructure.Models;

namespace GeoDataBusiness.Services;

public interface IBoundaryService
{
    Task CreateAsync(Boundary boundary);
    Task<Boundary?> GetAsync(int id);
    Task<IEnumerable<Boundary>> GetAsync();
    Task UpdateAsync(int id, Boundary boundary);
    Task DeleteAsync(int id);
}