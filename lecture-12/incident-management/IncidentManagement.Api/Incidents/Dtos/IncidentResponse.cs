using IncidentManagement.Domain.Incidents;

namespace IncidentManagement.Api.Incidents.Dtos;

public sealed record IncidentResponse
(
    Guid Id,
    string Title,
    string Description,
    IncidentPriority Priority,
    IncidentStatus Status,
    DateTimeOffset CreatedAtUtc,
    bool? NotificationSent = null
)
{
    public static IncidentResponse From(Incident incident, bool? notificationSent = null) =>
        new(
            incident.Id,
            incident.Title,
            incident.Description,
            incident.Priority,
            incident.Status,
            incident.CreatedAtUtc,
            notificationSent
        );
}