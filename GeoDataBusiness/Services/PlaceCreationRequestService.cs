using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;
using System.Data;
using System.Transactions;

namespace GeoDataBusiness.Services
{
    public class PlaceCreationRequestService : IPlaceCreationRequestService
    {
        private readonly IPlaceCreationRequestRepository _repository;
        public PlaceCreationRequestService(IPlaceCreationRequestRepository repository)
        {
            _repository = repository;
        }
        public Task CreateAsync(Place place, Name name, Location location)
        {
            return _repository.CreateAsync(place, name, location);
        }
    }
}
