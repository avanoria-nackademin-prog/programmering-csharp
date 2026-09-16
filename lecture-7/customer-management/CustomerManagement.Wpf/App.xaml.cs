using CustomerManagement.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CustomerManagement.Wpf;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddSingleton<ICustomerService, InMemoryCustomerService>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }

}
