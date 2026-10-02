using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;
using IncidentManagement.Domain.Incidents.Contracts;
using NSubstitute;

namespace IncidentManagement.Tests.Application.UnitTests;

public class IncidentServiceTests_NSubstitute
{
    [Fact]
    public async Task CreateAsync_Should_SaveIncidentAndSendNotification()
    {
        // Arrange
        var repository = Substitute.For<IIncidentRepository>();
        var notifier = Substitute.For<IIncidentNotifier>();

        repository
            .AddAsync(Arg.Any<Incident>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        
        notifier
            .NotifyCreatedAsync(Arg.Any<Incident>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        var service = new IncidentService(repository, notifier);
        var command = new CreateIncidentCommand("Kan inte logga in", "Jag får fel när jag försöker logga in.", IncidentPriority.High);

        // Act
        var result = await service.CreateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Incident.Id);
        Assert.Equal("Kan inte logga in", result.Incident.Title);
        Assert.Equal("Jag får fel när jag försöker logga in.", result.Incident.Description);
        Assert.Equal(IncidentStatus.New, result.Incident.Status);
        Assert.True(result.NotificationSent);

        await repository.Received(1).AddAsync
        (
            Arg.Is<Incident>(x => x.Id == result.Incident.Id),
            Arg.Any<CancellationToken>()
        );

        await notifier.Received(1).NotifyCreatedAsync
        (
            Arg.Is<Incident>(x => x.Id == result.Incident.Id),
            Arg.Any<CancellationToken>()
        );
    }
}
