using GeoDataInfrastructure.Models;
using System.Transactions;

namespace GeoDataBusiness.Services
{
    public interface IPlaceCreationRequestService
    {
        Task CreateAsync(Place place, Name name, Location location);
    }
}
