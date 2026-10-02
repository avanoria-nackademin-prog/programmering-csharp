using IncidentManagement.Domain.Incidents;

namespace IncidentManagement.Application.Incidents.Dtos;

public sealed record CreateIncidentResult
(
    Incident Incident,
    bool NotificationSent
);