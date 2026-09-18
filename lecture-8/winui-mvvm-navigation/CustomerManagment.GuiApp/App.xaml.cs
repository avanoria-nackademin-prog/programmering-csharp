using CustomerManagement.Application;
using CustomerManagement.Infrastructure;
using CustomerManagment.GuiApp.Navigation;
using CustomerManagment.GuiApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace CustomerManagment.GuiApp;


public partial class App : Application
{
    public static IServiceProvider Provider { get; private set; } = null!;
    private Window? _window;

    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();

        services.AddApplication();
        services.AddInfrastructure();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddTransient<HomeViewModel>();

        services.AddTransient<MainWindow>();

        Provider = services.BuildServiceProvider();

    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = Provider.GetRequiredService<MainWindow>();
        _window.Activate();
    }
}
