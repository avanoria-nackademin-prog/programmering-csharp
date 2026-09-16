using CustomerManagement.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();

        services.AddSingleton<ICustomerService, InMemoryCustomerService>();
        services.AddTransient<Form1>();

        using var serviceProvider = services.BuildServiceProvider();
        var form = serviceProvider.GetRequiredService<Form1>();

        Application.Run(form);
    }
}