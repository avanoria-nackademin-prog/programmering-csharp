using Microsoft.Extensions.DependencyInjection;

namespace CustomerApp.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}