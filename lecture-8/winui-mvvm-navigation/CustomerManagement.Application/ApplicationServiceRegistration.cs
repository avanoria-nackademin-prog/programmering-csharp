using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
