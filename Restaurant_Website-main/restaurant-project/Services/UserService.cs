using Npgsql;
using BCrypt.Net;
using restaurant_project.Models;
using Microsoft.Extensions.Configuration;

namespace restaurant_project.Services;


public class UserService
{
    public async Task<User?> LoginUser(string email, string password)
{
    using var conn = new NpgsqlConnection(_connectionString);
    await conn.OpenAsync();

    using var cmd = new NpgsqlCommand("SELECT id, email, password_hash FROM users WHERE email = @e", conn);
    cmd.Parameters.AddWithValue("e", email);

    using var reader = await cmd.ExecuteReaderAsync();
    if (await reader.ReadAsync())
    {
        var dbHash = reader.GetString(2);
        // important. verified the password against the hashed pashword in NEON -@mclark48
        if (BCrypt.Net.BCrypt.Verify(password, dbHash))
        {
            return new User 
            { 
                Id = reader.GetInt32(0), 
                Email = reader.GetString(1) 
            };
        }
    }
    return null; // Login failed -@mclark48
}
    private readonly string _connectionString;

    public UserService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<bool> RegisterUser(string email, string password)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        using var cmd = new NpgsqlCommand("INSERT INTO users (email, password_hash) VALUES (@e, @p)", conn);
        cmd.Parameters.AddWithValue("e", email);
        cmd.Parameters.AddWithValue("p", hash);

        try {
            await cmd.ExecuteNonQueryAsync();
            return true;
        } catch {
            return false; 
        }
    }
}