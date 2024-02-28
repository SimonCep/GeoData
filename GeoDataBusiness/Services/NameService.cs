using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;

namespace GeoDataBusiness.Services;

public class NameService : INameService
{
    private readonly INameRepository _repository;

    public NameService(INameRepository repository)
    {
        _repository = repository;
    }

    public Task CreateAsync(Name name)
    {
        return _repository.CreateAsync(name);
    }

    public Task<Name?> GetAsync(string value)
    {
        return _repository.GetAsync(value);
    }

    public Task<Name?> GetAsync(int placeId, string locale)
    {
        return _repository.GetAsync(placeId, locale);
    }

    public Task<IEnumerable<Name>> GetAsync(string value, string locale)
    {
        if (value.Length >= 3)
            return _repository.GetAsync(value, locale);

        return Task.FromResult(Enumerable.Empty<Name>());
    }

    public Task<IEnumerable<Name>> GetAsync()
    {
        return _repository.GetAsync();
    }

    public Task<IEnumerable<Name>> GetByPlaceIdAsync(int placeId)
    {
        return _repository.GetByPlaceIdAsync(placeId);
    }

    public Task UpdateAsync(int id, Name name)
    {
        return _repository.UpdateAsync(id, name);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}