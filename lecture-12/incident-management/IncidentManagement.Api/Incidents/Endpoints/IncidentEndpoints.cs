using IncidentManagement.Api.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Dtos;
using IncidentManagement.Application.Incidents.Services;
using IncidentManagement.Domain.Incidents;

namespace IncidentManagement.Api.Incidents.Endpoints;

public static class IncidentEndpoints
{
    public static IEndpointRouteBuilder MapIncidentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/incidents");

        group.MapPost("", CreateIncident);
        group.MapGet("", GetAllIncidents);
        group.MapGet("/{id:guid}", GetIncidentById);

        return endpoints;
    }

    private static async Task<IResult> CreateIncident(CreateIncidentRequest request, IIncidentService incidentService, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<IncidentPriority>(request.Priority, ignoreCase: true, out var priority) || !Enum.IsDefined(priority))
        {
            return Results.BadRequest(new
            {
                error = "Priority must be Low, Normal, High, or Critical."
            });
        }

        try
        {
            var command = new CreateIncidentCommand(request.Title, request.Description, priority);

            var result = await incidentService.CreateAsync(command, cancellationToken);

            return Results.Created($"/api/incidents/{result.Incident.Id}",
                IncidentResponse.From(result.Incident, result.NotificationSent));
        }
        catch (ArgumentException exception)
        {
            return Results.BadRequest(new { error = exception.Message });
        }
    }

    private static async Task<IResult> GetAllIncidents(IIncidentService incidentService, CancellationToken cancellationToken)
    {
        var incidents = await incidentService.GetAllAsync(cancellationToken);

        return Results.Ok(incidents.Select(incident => IncidentResponse.From(incident)));
    }

    private static async Task<IResult> GetIncidentById(Guid id, IIncidentService incidentService, CancellationToken cancellationToken)
    {
        var incident = await incidentService.GetByIdAsync(id, cancellationToken);

        return incident is null
            ? Results.NotFound()
            : Results.Ok(IncidentResponse.From(incident));
    }
}