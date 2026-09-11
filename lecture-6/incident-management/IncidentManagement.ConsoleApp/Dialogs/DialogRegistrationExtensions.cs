using IncidentManagement.ConsoleApp.Dialogs.Customers;
using IncidentManagement.ConsoleApp.Dialogs.Menu;
using Microsoft.Extensions.DependencyInjection;

namespace IncidentManagement.ConsoleApp.Dialogs;

internal static class DialogRegistrationExtensions
{
    public static IServiceCollection AddDialogs(this IServiceCollection services)
    {
        services.AddTransient<IAppDialog, ApplicationDialog>();
        services.AddTransient<IMenuDialog, MenuDialog>();
        services.AddTransient<CustomerDialog>();

        return services;
    }
}
