namespace IncidentManagement.Api.Incidents.Dtos;

public sealed record CreateIncidentRequest
(
    string Title,
    string Description,
    string Priority
);
