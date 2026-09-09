using IncidentSystem.Domain.Customers;
using IncidentSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentSystem.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();

        return services;
    }
}