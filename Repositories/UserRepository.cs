using BrabantCareWebApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

public class UserRepository
{
    private readonly string _connectionString;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(string connectionString, ILogger<UserRepository> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var users = await connection.QueryAsync<User>("SELECT Id, Email FROM AspNetUsers");
        return users.AsList();
    }

    public async Task DeleteUserAsync(string userId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM AspNetUsers WHERE Id = @UserId", new { UserId = userId });
    }
}
