using IncidentManagement.Domain.Incidents.Contracts;
using IncidentManagement.Infrastructure.Notifications;
using IncidentManagement.Infrastructure.Persistence;
using IncidentManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentManagement.Infrastructure;

public static class InfrastructureRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connectionString = "Data Source=incidents.db";

        var notificationBaseUrl = "http://localhost:5099";

        services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IIncidentRepository, SqliteIncidentRepository>();

        services.AddHttpClient<IIncidentNotifier, HttpIncidentNotifier>(client =>
        {
            client.BaseAddress = new Uri(notificationBaseUrl);
        });

        return services;
    }
}
