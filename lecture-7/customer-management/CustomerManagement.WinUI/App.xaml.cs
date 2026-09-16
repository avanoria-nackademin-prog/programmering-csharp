using CustomerManagement.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;


namespace CustomerManagement.WinUI;


public partial class App : Application
{
    private Window? _window;
    private ServiceProvider _serviceProvider;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var services = new ServiceCollection();

        services.AddSingleton<ICustomerService, InMemoryCustomerService>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();

        _window = _serviceProvider.GetRequiredService<MainWindow>();
        _window.Activate();
    }
}
