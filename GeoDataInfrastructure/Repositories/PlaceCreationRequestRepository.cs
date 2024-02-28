using GeoDataInfrastructure.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
/*
 * 
{
  "id": 0,
  "population": 542,
  "rating": 3,
  "hierarchy": "5",
  "name": {
    "id": 0,
    "locale": "en",
    "value": "Ignmiestis",
    "placeId": 0
  },
  "location": {
    "id": 0,
    "latitude": 35.267,
    "longitude": 78.3214,
    "altitude": 123,
    "placeId": 0
  }
}
 */

namespace GeoDataInfrastructure.Repositories
{
    public class PlaceCreationRequestRepository : IPlaceCreationRequestRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly PlaceRepository _placeRepository;
        private readonly NameRepository _nameRepository;
        private readonly LocationRepository _locationRepository;

        public PlaceCreationRequestRepository(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
            _placeRepository = new PlaceRepository(_mySqlConnection);
            _nameRepository = new NameRepository(_mySqlConnection);
            _locationRepository = new LocationRepository(_mySqlConnection);
        }
        public async Task CreateAsync(Place place, Name name, Location location)
        {
            _mySqlConnection.Open();
            using (var transaction = _mySqlConnection.BeginTransaction())
            {
                try
                {
                    int lastId = await _placeRepository.CreateAsync(place, transaction);
                    name.PlaceId = lastId;
                    await _nameRepository.CreateAsync(name, transaction);
                    location.PlaceId = lastId;
                    await _locationRepository.CreateAsync(location, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                
            }
        }
    }
}
