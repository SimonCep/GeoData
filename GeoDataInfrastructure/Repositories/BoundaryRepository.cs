using Dapper;
using GeoDataInfrastructure.Models;
using MySqlConnector;

namespace GeoDataInfrastructure.Repositories;

public class BoundaryRepository : IBoundaryRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public BoundaryRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task CreateAsync(Boundary boundary)
    {
        const string sql = "insert into boundaries values (NULL, @geojson, @place_id)";
        await _mySqlConnection.QueryAsync(sql, new { boundary.GeoJson, boundary.PlaceId });
    }

    public async Task<Boundary?> GetAsync(int placeId)
    {
        const string sql = "select * from boundaries where place_id = @placeId";
        Boundary boundary = await _mySqlConnection.QueryFirstOrDefaultAsync<Boundary>(sql, new { placeId });
        return boundary;
    }

    public async Task<IEnumerable<Boundary>> GetAsync()
    {
        const string sql = "select * from boundaries";
        IEnumerable<Boundary> boundaries = await _mySqlConnection.QueryAsync<Boundary>(sql);
        return boundaries;
    }

    public async Task UpdateAsync(int id, Boundary boundary)
    {
        if (await PlaceExistsAsync(id))
        {
            const string sql =
                "update boundaries set geojson = @geojson, place_id = @placeId where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { boundary.GeoJson, boundary.PlaceId, id });
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (await PlaceExistsAsync(id))
        {
            const string sql = "delete from boundaries where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { id });
        }
    }

    private async Task<bool> PlaceExistsAsync(int boundaryId)
    {
        const string sql = "select id from boundaries where id = @boundaryId";
        int id = await _mySqlConnection.QueryFirstOrDefaultAsync<int>(sql, new { boundaryId });
        return id != default;
    }
}