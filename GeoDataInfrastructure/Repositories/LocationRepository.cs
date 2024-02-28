using Dapper;
using GeoDataInfrastructure.Models;
using MySqlConnector;
using System.Data;

namespace GeoDataInfrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public LocationRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task CreateAsync(Location location, IDbTransaction? transaction = null)
    {
        const string sql = "insert into locations values (NULL, @latitude, @longitude, @altitude, @placeId)";
        if (transaction is null)
        {
            await _mySqlConnection.QueryAsync(sql, new { location.Latitude, location.Longitude, location.Altitude, location.PlaceId });
            return;
        }
        await _mySqlConnection.QueryAsync(sql, new { location.Latitude, location.Longitude, location.Altitude, location.PlaceId }, 
            transaction: transaction);
    }

    public async Task<Location?> GetAsync(int id)
    {
        const string sql = "select * from locations where id = @id";
        Location location = await _mySqlConnection.QueryFirstOrDefaultAsync<Location>(sql, new { id });
        return location;
    }

    public async Task<IEnumerable<Location>> GetAsync()
    {
        const string sql = "select * from locations";
        IEnumerable<Location> locations = await _mySqlConnection.QueryAsync<Location>(sql);
        return locations;
    }

    public async Task UpdateAsync(int id, Location location)
    {
        if (await PlaceExistsAsync(id))
        {
            const string sql =
                "update locations set latitude = @latitude, longitude = @longitude, altitude = @altitude, placeid = @placeid where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { location.Latitude, location.Longitude, location.Altitude, location.PlaceId, id });
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (await PlaceExistsAsync(id))
        {
            const string sql = "delete from places where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { id });
        }
    }

    private async Task<bool> PlaceExistsAsync(int placeId)
    {
        const string sql = "select id from places where id = @placeId";
        int id = await _mySqlConnection.QueryFirstOrDefaultAsync<int>(sql, new { placeId });
        return id != default;
    }
}