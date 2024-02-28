using Dapper;
using GeoDataInfrastructure.Models;
using MySqlConnector;
using System.Data;
using System.Transactions;

namespace GeoDataInfrastructure.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public PlaceRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task<int> CreateAsync(Place place, IDbTransaction? transaction = null)
    {
        const string sql = "insert into places values (NULL, @population, @rating, @hierarchy, 1); SELECT LAST_INSERT_ID()";
        if (transaction is null)
        {
            await _mySqlConnection.QueryAsync(sql, new { place.Population, place.Rating, place.Hierarchy });
            return -1;
        }
        int newPlaceId = await _mySqlConnection.ExecuteScalarAsync<int>(sql, new { place.Population, place.Rating, place.Hierarchy }, transaction: transaction);
        return newPlaceId;
            
    }

    public async Task<Place?> GetAsync(int id)
    {
        const string sql = "select * from places where id = @id";
        Place place = await _mySqlConnection.QueryFirstOrDefaultAsync<Place>(sql, new { id });
        return place;
    }

    public async Task<IEnumerable<Place>> GetAsync()
    {
        const string sql = "select * from places";
        IEnumerable<Place> places = await _mySqlConnection.QueryAsync<Place>(sql);
        return places;
    }

    public async Task UpdateAsync(int id, Place place)
    {
        if (await PlaceExistsAsync(id))
        {
            const string sql =
                "update places set population = @population, rating = @rating, hierarchy = @hierarchy where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { place.Population, place.Rating, place.Hierarchy, id });
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