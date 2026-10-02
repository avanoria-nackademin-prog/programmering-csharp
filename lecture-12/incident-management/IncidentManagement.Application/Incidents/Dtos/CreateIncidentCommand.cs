using IncidentManagement.Domain.Incidents;

namespace IncidentManagement.Application.Incidents.Dtos;

public sealed record CreateIncidentCommand
(
    string Title,
    string Description,
    IncidentPriority Priority
);
