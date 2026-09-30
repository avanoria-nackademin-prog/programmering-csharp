using Microsoft.Extensions.DependencyInjection;

namespace CustomerApp.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string customersFilePath)
    {
        return services;
    }
}
