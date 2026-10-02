using IncidentManagement.Domain.Incidents;
using IncidentManagement.Domain.Incidents.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.Infrastructure.Persistence.Repositories;

public sealed class SqliteIncidentRepository(DataContext context) : IIncidentRepository
{
    public async Task AddAsync(Incident incident, CancellationToken cancellationToken)
    {
        context.Incidents.Add(incident);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        context.Incidents.SingleOrDefaultAsync(
            incident => incident.Id == id,
            cancellationToken);

    public async Task<IReadOnlyList<Incident>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Incidents
            .OrderByDescending(incident => incident.CreatedAtUtc)
            .ToListAsync(cancellationToken);
}