using IncidentManagement.Application.Incidents.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentManagement.Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IIncidentService, IncidentService>();

        return services;
    }
}
