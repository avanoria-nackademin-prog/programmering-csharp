using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Domain.Incidents;

namespace IncidentManagement.Application.Incidents.Services;

public interface IIncidentService
{
    Task<CreateIncidentResult> CreateAsync(CreateIncidentCommand command, CancellationToken cancellationToken);
    Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Incident>> GetAllAsync(CancellationToken cancellationToken);
}
