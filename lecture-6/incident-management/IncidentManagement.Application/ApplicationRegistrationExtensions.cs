using IncidentManagement.Application.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentManagement.Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerSerivce, CustomerService>();

        return services;
    }
}
