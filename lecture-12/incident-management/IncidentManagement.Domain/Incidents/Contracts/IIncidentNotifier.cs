namespace IncidentManagement.Domain.Incidents.Contracts;

public interface IIncidentNotifier
{
    Task<bool> NotifyCreatedAsync(Incident incident, CancellationToken cancellationToken);
}