using IncidentManagement.Domain.Incidents;
using IncidentManagement.Infrastructure.Persistence.Repositories;

namespace IncidentManagement.Tests.Infrastructure.IntegrationTests;

public class SqliteIncidentRepositoryTests
{
    [Fact]
    public async Task AddAsync_Should_SaveIncidentThatCanBeReadBack()
    {
        // Arrange
        await using var database = await SqliteTestDatabase.CreateAsync();
        var incident = CreateIncident();

        // Act
        await using (var context = database.CreateDbContext())
        {
            var repository = new SqliteIncidentRepository(context);

            await repository.AddAsync(incident, CancellationToken.None);
        }

        // Assert
        await using var readDbContext = database.CreateDbContext();
        var readRepository = new SqliteIncidentRepository(readDbContext);

        var savedIncident = await readRepository.GetByIdAsync(incident.Id, CancellationToken.None);

        Assert.NotNull(savedIncident);
        Assert.Equal(incident.Id, savedIncident.Id);
        Assert.Equal(incident.Title, savedIncident.Title);
        Assert.Equal(incident.Description, savedIncident.Description);
        Assert.Equal(incident.Priority, savedIncident.Priority);
        Assert.Equal(incident.Status, savedIncident.Status);
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnAllSavedIncidents()
    {
        // Arrange
        await using var database = await SqliteTestDatabase.CreateAsync();
        var firstIncident = CreateIncident("Cannot sign in");
        var secondIncident = CreateIncident("Printer unavailable");

        await using (var context = database.CreateDbContext())
        {
            var repository = new SqliteIncidentRepository(context);

            await repository.AddAsync(firstIncident, CancellationToken.None);
            await repository.AddAsync(secondIncident, CancellationToken.None);
        }

        // Act
        await using var readDbContext = database.CreateDbContext();
        var readRepository = new SqliteIncidentRepository(readDbContext);

        var incidents = await readRepository.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.Equal(2, incidents.Count);
        Assert.Contains(incidents, incident => incident.Id == firstIncident.Id);
        Assert.Contains(incidents, incident => incident.Id == secondIncident.Id);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnNullWhenIncidentDoesNotExist()
    {
        // Arrange
        await using var database = await SqliteTestDatabase.CreateAsync();
        await using var context = database.CreateDbContext();
        var repository = new SqliteIncidentRepository(context);

        // Act
        var result = await repository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    private static Incident CreateIncident(string title = "Unable to sign in") =>
        Incident.Create(title, "The user receives an error when signing in.", IncidentPriority.High);
}
