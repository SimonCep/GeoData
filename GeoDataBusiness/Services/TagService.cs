using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;

namespace GeoDataBusiness.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public Task CreateAsync(Tag tag)
    {
        return _repository.CreateAsync(tag);
    }

    public Task<Tag?> GetAsync(int id)
    {
        return _repository.GetAsync(id);
    }

    public Task<IEnumerable<Tag>> GetAsync()
    {
        return _repository.GetAsync();
    }

    public Task<IEnumerable<Tag>> GetByPlaceIdAsync(int placeId)
    {
        return _repository.GetByPlaceIdAsync(placeId);
    }

    public Task UpdateAsync(int id, Tag tag)
    {
        return _repository.UpdateAsync(id, tag);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}