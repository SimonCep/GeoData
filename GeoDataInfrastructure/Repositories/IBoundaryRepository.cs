using GeoDataInfrastructure.Models;

namespace GeoDataInfrastructure.Repositories;

public interface IBoundaryRepository
{
    Task CreateAsync(Boundary boundary);
    Task<Boundary?> GetAsync(int id);
    Task<IEnumerable<Boundary>> GetAsync();
    Task UpdateAsync(int id, Boundary boundary);
    Task DeleteAsync(int id);
}