using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;
using IncidentManagement.Domain.Incidents.Contracts;
using IncidentManagement.Infrastructure.Persistence.Repositories;
using IncidentManagement.Tests.Infrastructure.IntegrationTests;
using NSubstitute;

namespace IncidentManagement.Tests.Application.IntegrationTests;

public class IncidentServiceTests
{

    [Fact]
    public async Task CreateAsync_Should_SaveIncidentAndSendNotification()
    {
        // Arrange
        var notifier = Substitute.For<IIncidentNotifier>();
        notifier.NotifyCreatedAsync(Arg.Any<Incident>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

        var command = new CreateIncidentCommand("Kan inte logga in", "Jag får fel när jag försöker logga in.", IncidentPriority.High);

        await using var database = await SqliteTestDatabase.CreateAsync();

        Incident createdIncident;

        // Act
        await using (var writeDbContext = database.CreateDbContext())
        {
            var repository = new SqliteIncidentRepository(writeDbContext);
            var service = new IncidentService(repository, notifier);

            var result = await service.CreateAsync(command, CancellationToken.None);

            createdIncident = result.Incident;
        }

        // Assert
        await using var readDbContext = database.CreateDbContext();
        var readRepository = new SqliteIncidentRepository(readDbContext);

        var savedIncident = await readRepository.GetByIdAsync(createdIncident.Id, CancellationToken.None);

        Assert.NotNull(savedIncident);
        Assert.Equal(createdIncident.Id, savedIncident.Id);
        Assert.Equal("Kan inte logga in", savedIncident.Title);
        Assert.Equal("Jag får fel när jag försöker logga in.", savedIncident.Description);
        Assert.Equal(IncidentPriority.High, savedIncident.Priority);
        Assert.Equal(IncidentStatus.New, savedIncident.Status);

        await notifier.Received(1).NotifyCreatedAsync(
            Arg.Is<Incident>(incident => incident.Id == createdIncident.Id),
            Arg.Any<CancellationToken>());
    }
}
