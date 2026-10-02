using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace IncidentManagement.UI.ViewModels;

public partial class MainViewModel(IIncidentService incidentService)
    : ObservableObject
{
    public ObservableCollection<Incident> Incidents { get; } = [];

    public IReadOnlyList<string> PriorityOptions { get; } =
        Enum.GetNames<IncidentPriority>();

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SelectedPriority { get; set; } =
        nameof(IncidentPriority.Normal);

    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        try
        {
            IsBusy = true;
            Message = string.Empty;

            await RefreshIncidentsAsync(cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Användaren stängde fönstret eller avbröt inläsningen.
        }
        catch (Exception exception)
        {
            Message = $"Kunde inte läsa incidenterna: {exception.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task CreateIncidentAsync(CancellationToken cancellationToken)
    {
        try
        {
            IsBusy = true;
            Message = string.Empty;

            var priority = Enum.Parse<IncidentPriority>(SelectedPriority);

            await incidentService.CreateAsync(
                new CreateIncidentCommand(Title, Description, priority),
                cancellationToken);

            Title = string.Empty;
            Description = string.Empty;
            SelectedPriority = nameof(IncidentPriority.Normal);

            await RefreshIncidentsAsync(cancellationToken);

            Message = "Incidenten har sparats.";
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            Message = "Åtgärden avbröts.";
        }
        catch (ArgumentException exception)
        {
            Message = exception.Message;
        }
        catch (Exception exception)
        {
            Message = $"Kunde inte spara incidenten: {exception.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task RefreshIncidentsAsync(CancellationToken cancellationToken)
    {
        var incidents = await incidentService.GetAllAsync(cancellationToken);

        Incidents.Clear();

        foreach (var incident in incidents)
            Incidents.Add(incident);
    }
}