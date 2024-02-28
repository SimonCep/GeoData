using Dapper;
using GeoDataInfrastructure.Models;
using MySqlConnector;

namespace GeoDataInfrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public UserRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task CreateAsync(User user)
    {
        const string sql = "insert into users values (NULL, @email, @hashedPassword)";
        await _mySqlConnection.QueryAsync(sql, new { user.Email, user.HashedPassword });
    }

    public async Task<User?> GetAsync(string email)
    {
        const string sql = "select * from users where email = @email";
        return await _mySqlConnection.QueryFirstOrDefaultAsync<User>(sql, new { email });
    }
}