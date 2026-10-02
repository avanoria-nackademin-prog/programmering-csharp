using IncidentManagement.Domain.Incidents;
using IncidentManagement.Domain.Incidents.Contracts;
using System.Net.Http.Json;

namespace IncidentManagement.Infrastructure.Notifications;

public sealed class HttpIncidentNotifier(HttpClient httpClient) : IIncidentNotifier
{
    public async Task<bool> NotifyCreatedAsync(Incident incident, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/notifications",
                new
                {
                    incident.Id,
                    incident.Title,
                    incident.Priority,
                    incident.CreatedAtUtc
                },
                cancellationToken);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}
