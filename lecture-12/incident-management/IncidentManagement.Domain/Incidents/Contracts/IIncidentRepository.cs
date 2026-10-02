namespace IncidentManagement.Domain.Incidents.Contracts;

public interface IIncidentRepository
{
    Task AddAsync(Incident incident, CancellationToken cancellationToken);
    Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Incident>> GetAllAsync(CancellationToken cancellationToken);
}