using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;
using IncidentManagement.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IncidentManagement.UI.Tests.UnitTests;

[TestClass]
public class MainViewModelTests
{
    [TestMethod]
    public async Task LoadAsync_Should_LoadIncidentsAndPopulateCollection()
    {
        // Arrange
        var incidentService = Substitute.For<IIncidentService>();
        var incidents = new[]
        {
            Incident.Create(
                "Cannot sign in",
                "The user cannot sign in.",
                IncidentPriority.High),
            Incident.Create(
                "Slow response",
                "The app responds slowly.",
                IncidentPriority.Normal)
        };

        incidentService
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IReadOnlyList<Incident>>(incidents));

        var viewModel = new MainViewModel(incidentService);
        using var cancellationTokenSource = new CancellationTokenSource();

        // Act
        await viewModel.LoadAsync(cancellationTokenSource.Token);

        // Assert
        Assert.HasCount(incidents.Length, viewModel.Incidents);
        Assert.AreSame(incidents[0], viewModel.Incidents[0]);
        Assert.AreSame(incidents[1], viewModel.Incidents[1]);
        Assert.AreEqual(string.Empty, viewModel.Message);
        Assert.IsFalse(viewModel.IsBusy);

        await incidentService.Received(1)
            .GetAllAsync(cancellationTokenSource.Token);
    }

    [TestMethod]
    public async Task CreateIncidentCommand_Should_CreateIncidentAndRefreshList()
    {
        // Arrange
        var incidentService = Substitute.For<IIncidentService>();
        var createdIncident = Incident.Create(
            "Cannot sign in",
            "The user cannot sign in.",
            IncidentPriority.High);

        var createIncidentResult = new CreateIncidentResult(createdIncident, true);

        incidentService
            .CreateAsync(
                Arg.Any<CreateIncidentCommand>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(createIncidentResult));

        incidentService
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IReadOnlyList<Incident>>([createdIncident]));

        var viewModel = new MainViewModel(incidentService)
        {
            Title = "Cannot sign in",
            Description = "The user cannot sign in.",
            SelectedPriority = nameof(IncidentPriority.High)
        };

        // Act
        await viewModel.CreateIncidentCommand.ExecuteAsync(null);

        // Assert
        await incidentService.Received(1).CreateAsync(
            Arg.Is<CreateIncidentCommand>(request =>
                request.Title == "Cannot sign in" &&
                request.Description == "The user cannot sign in." &&
                request.Priority == IncidentPriority.High),
            Arg.Any<CancellationToken>());

        Assert.IsTrue(viewModel.Incidents.Contains(createdIncident));
        Assert.AreEqual(string.Empty, viewModel.Title);
        Assert.AreEqual(string.Empty, viewModel.Description);
        Assert.AreEqual(
            nameof(IncidentPriority.Normal),
            viewModel.SelectedPriority);
        Assert.AreEqual("Incidenten har sparats.", viewModel.Message);
        Assert.IsFalse(viewModel.IsBusy);
    }

    [TestMethod]
    public async Task CreateIncidentCommand_Should_SetMessageWhenServiceReturnsArgumentException()
    {
        // Arrange
        var incidentService = Substitute.For<IIncidentService>();

        incidentService
            .CreateAsync(
                Arg.Any<CreateIncidentCommand>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromException<CreateIncidentResult>(
                new ArgumentException("Incident title is required.")));

        var viewModel = new MainViewModel(incidentService)
        {
            Title = string.Empty,
            Description = "The user cannot sign in.",
            SelectedPriority = nameof(IncidentPriority.High)
        };

        // Act
        await viewModel.CreateIncidentCommand.ExecuteAsync(null);

        // Assert
        Assert.AreEqual("Incident title is required.", viewModel.Message);
        Assert.IsFalse(viewModel.IsBusy);

        await incidentService.DidNotReceive()
            .GetAllAsync(Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task LoadAsync_Should_NotShowErrorWhenOperationIsCancelled()
    {
        // Arrange
        var incidentService = Substitute.For<IIncidentService>();
        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        incidentService
            .GetAllAsync(cancellationTokenSource.Token)
            .Returns(Task.FromCanceled<IReadOnlyList<Incident>>(
                cancellationTokenSource.Token));

        var viewModel = new MainViewModel(incidentService);

        // Act
        await viewModel.LoadAsync(cancellationTokenSource.Token);

        // Assert
        Assert.AreEqual(string.Empty, viewModel.Message);
        Assert.IsFalse(viewModel.IsBusy);
    }
}
