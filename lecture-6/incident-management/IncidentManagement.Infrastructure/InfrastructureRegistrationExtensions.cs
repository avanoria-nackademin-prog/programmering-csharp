using IncidentManagement.Domain.Customers;
using IncidentManagement.Infrastructure.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentManagement.Infrastructure;

public static class InfrastructureRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerStore, InMemoryCustomerStore>();

        return services;
    }
}
