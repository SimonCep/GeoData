using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;

namespace GeoDataBusiness.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _repository;

    public LocationService(ILocationRepository repository)
    {
        _repository = repository;
    }

    public Task CreateAsync(Location location)
    {
        return _repository.CreateAsync(location);
    }

    public Task<Location?> GetAsync(int id)
    {
        return _repository.GetAsync(id);
    }

    public Task<IEnumerable<Location>> GetAsync()
    {
        return _repository.GetAsync();
    }

    public Task UpdateAsync(int id, Location location)
    {
        return _repository.UpdateAsync(id, location);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}