using Dapper;
using GeoDataInfrastructure.Models;
using MySqlConnector;
using System.Data;

namespace GeoDataInfrastructure.Repositories;

public class NameRepository : INameRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public NameRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task CreateAsync(Name name, IDbTransaction? transaction = null)
    {
        const string sql = "insert into names values (NULL, @locale, @value, @placeId)";
        if (transaction is null)
        {
            await _mySqlConnection.QueryAsync(sql, new { name.Locale, name.Value, name.PlaceId });
        }
        else await _mySqlConnection.QueryAsync(sql, new { name.Locale, name.Value, name.PlaceId }, transaction: transaction);
    }

    public async Task<Name?> GetAsync(string value)
    {
        const string sql = "select * from names where value = @value";
        Name name = await _mySqlConnection.QueryFirstOrDefaultAsync<Name>(sql, new { value });
        return name;
    }

    public async Task<Name?> GetAsync(int place_id, string locale)
    {
        const string sql = "select * from names where place_id = @place_id AND locale = @locale";
        Name name = await _mySqlConnection.QueryFirstOrDefaultAsync<Name>(sql, new { place_id, locale });
        return name;
    }

    public async Task<IEnumerable<Name>> GetAsync(string value, string locale)
    {
        const string sql = "SELECT * FROM names WHERE `value` LIKE CONCAT('%', @value, '%') AND locale = @locale";
        IEnumerable<Name> names = await _mySqlConnection.QueryAsync<Name>(sql, new { value, locale });
        return names;
    }

    public async Task<IEnumerable<Name>> GetAsync()
    {
        const string sql = "select * from names";
        IEnumerable<Name> names = await _mySqlConnection.QueryAsync<Name>(sql);
        return names;
    }

    public async Task<IEnumerable<Name>> GetByPlaceIdAsync(int placeId)
    {
        const string sql = "select * from names where place_id = @placeId";
        IEnumerable<Name> names = await _mySqlConnection.QueryAsync<Name>(sql, new { placeId });
        return names;
    }

    public async Task UpdateAsync(int id, Name name)
    {
        if (await NameExistsAsync(id))
        {
            const string sql =
                "update names set locale = @locale, value = @value, place_id = @placeId where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { name.Locale, name.Value, name.PlaceId, id });
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (await NameExistsAsync(id))
        {
            const string sql = "delete from names where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { id });
        }
    }

    private async Task<bool> NameExistsAsync(int nameId)
    {
        const string sql = "select id from names where id = @nameId";
        int id = await _mySqlConnection.QueryFirstOrDefaultAsync<int>(sql, new { nameId });
        return id != default;
    }
}