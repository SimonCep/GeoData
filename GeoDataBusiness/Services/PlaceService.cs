using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;

namespace GeoDataBusiness.Services;

public class PlaceService : IPlaceService
{
    private readonly IPlaceRepository _repository;

    public PlaceService(IPlaceRepository repository)
    {
        _repository = repository;
    }

    public Task CreateAsync(Place place)
    {
        return _repository.CreateAsync(place);
    }

    public Task<Place?> GetAsync(int id)
    {
        return _repository.GetAsync(id);
    }

    public Task<IEnumerable<Place>> GetAsync()
    {
        return _repository.GetAsync();
    }

    public Task UpdateAsync(int id, Place place)
    {
        return _repository.UpdateAsync(id, place);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}