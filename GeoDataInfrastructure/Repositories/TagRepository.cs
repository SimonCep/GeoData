using Dapper;
using GeoDataInfrastructure.Models;
using MySqlConnector;

namespace GeoDataInfrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public TagRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task CreateAsync(Tag tag)
    {
        const string sql = "insert into tags values (NULL, @key, @value, @placeId)";
        await _mySqlConnection.QueryAsync(sql, new { tag.Key, tag.Value, tag.PlaceId });
    }

    public async Task<Tag?> GetAsync(int id)
    {
        const string sql = "select * from tags where id = @id";
        Tag tag = await _mySqlConnection.QueryFirstOrDefaultAsync<Tag>(sql, new { id });
        return tag;
    }

    public async Task<IEnumerable<Tag>> GetAsync()
    {
        const string sql = "select * from tags";
        IEnumerable<Tag> tags = await _mySqlConnection.QueryAsync<Tag>(sql);
        return tags;
    }

    public async Task<IEnumerable<Tag>> GetByPlaceIdAsync(int placeId)
    {
        const string sql = "select * from tags where place_id = @placeId";
        IEnumerable<Tag> tags = await _mySqlConnection.QueryAsync<Tag>(sql, new { placeId });
        return tags;
    }

    public async Task UpdateAsync(int id, Tag tag)
    {
        if (await TagExistsAsync(id))
        {
            const string sql =
                "update tags set key = @key, value = @value, place_id = @placeId where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { tag.Key, tag.Value, tag.PlaceId, id });
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (await TagExistsAsync(id))
        {
            const string sql = "delete from tags where id = @id";
            await _mySqlConnection.QueryAsync(sql, new { id });
        }
    }

    private async Task<bool> TagExistsAsync(int tagId)
    {
        const string sql = "select id from tags where id = @tagId";
        int id = await _mySqlConnection.QueryFirstOrDefaultAsync<int>(sql, new { tagId });
        return id != default;
    }
}