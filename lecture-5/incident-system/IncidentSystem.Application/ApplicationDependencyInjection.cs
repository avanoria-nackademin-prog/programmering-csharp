using IncidentSystem.Application.Features.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentSystem.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();

        return services;
    }
}
