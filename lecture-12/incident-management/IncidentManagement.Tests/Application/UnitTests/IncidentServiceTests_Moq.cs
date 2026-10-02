using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;
using IncidentManagement.Domain.Incidents.Contracts;
using Moq;

namespace IncidentManagement.Tests.Application.UnitTests;

public class IncidentServiceTests_Moq
{
    [Fact]
    public async Task CreateAsync_Should_SaveIncidentAndSendNotification()
    {
        // Arrange
        var repositoryMock = new Mock<IIncidentRepository>();
        var notifierMock = new Mock<IIncidentNotifier>();

        repositoryMock
            .Setup(repository => repository.AddAsync(It.IsAny<Incident>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        notifierMock
            .Setup(notifier => notifier.NotifyCreatedAsync(It.IsAny<Incident>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var service = new IncidentService(repositoryMock.Object, notifierMock.Object);
        var command = new CreateIncidentCommand("Kan inte logga in", "Jag får fel när jag försöker logga in.", IncidentPriority.High);

        // Act
        var result = await service.CreateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Incident.Id);
        Assert.Equal("Kan inte logga in", result.Incident.Title);
        Assert.Equal("Jag får fel när jag försöker logga in.", result.Incident.Description);
        Assert.Equal(IncidentStatus.New, result.Incident.Status);
        Assert.True(result.NotificationSent);

        repositoryMock.Verify(repository => repository.AddAsync(
            It.Is<Incident>(x => x.Id == result.Incident.Id),
            It.IsAny<CancellationToken>()), Times.Once);

        notifierMock.Verify(notifier => notifier.NotifyCreatedAsync(
            It.Is<Incident>(x => x.Id == result.Incident.Id),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
