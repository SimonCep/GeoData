using System.Transactions;
using GeoDataInfrastructure.Models;

namespace GeoDataInfrastructure.Repositories
{
    public interface IPlaceCreationRequestRepository
    {
        Task CreateAsync(Place place, Name name, Location location);
    }
}
