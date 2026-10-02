using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Domain.Incidents;
using IncidentManagement.Domain.Incidents.Contracts;

namespace IncidentManagement.Application.Incidents.Services;

public sealed class IncidentService(
    IIncidentRepository repository,
    IIncidentNotifier notifier) : IIncidentService
{
    public async Task<CreateIncidentResult> CreateAsync(
        CreateIncidentCommand command,
        CancellationToken cancellationToken)
    {
        var incident = Incident.Create(
            command.Title,
            command.Description,
            command.Priority);

        await repository.AddAsync(incident, cancellationToken);

        var notificationSent =
            await notifier.NotifyCreatedAsync(incident, cancellationToken);

        return new CreateIncidentResult(incident, notificationSent);
    }

    public Task<Incident?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Incident>> GetAllAsync(
        CancellationToken cancellationToken) =>
        repository.GetAllAsync(cancellationToken);
}