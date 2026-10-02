using IncidentManagement.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.Tests.Infrastructure.IntegrationTests;

internal sealed class SqliteTestDatabase : IAsyncDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<DataContext> _options;

    private SqliteTestDatabase(SqliteConnection connection)
    {
        _connection = connection;
        _options = new DbContextOptionsBuilder<DataContext>().UseSqlite(_connection).Options;
    }

    public static async Task<SqliteTestDatabase> CreateAsync()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var database = new SqliteTestDatabase(connection);

        await using var dbContext = database.CreateDbContext();
        await dbContext.Database.EnsureCreatedAsync();

        return database;
    }

    public DataContext CreateDbContext() => new(_options);

    public ValueTask DisposeAsync() => _connection.DisposeAsync();
}