using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;

namespace GeoDataBusiness.Services;

public class BoundaryService : IBoundaryService
{
    private readonly IBoundaryRepository _repository;

    public BoundaryService(IBoundaryRepository repository)
    {
        _repository = repository;
    }

    public Task CreateAsync(Boundary boundary)
    {
        return _repository.CreateAsync(boundary);
    }

    public Task<Boundary?> GetAsync(int id)
    {
        return _repository.GetAsync(id);
    }

    public Task<IEnumerable<Boundary>> GetAsync()
    {
        return _repository.GetAsync();
    }

    public Task UpdateAsync(int id, Boundary boundary)
    {
        return _repository.UpdateAsync(id, boundary);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}