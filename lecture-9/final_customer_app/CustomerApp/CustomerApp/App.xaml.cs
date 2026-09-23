using CustomerApp.Navigation;
using CustomerApp.Repositories;
using CustomerApp.Services;
using CustomerApp.ViewModels;
using CustomerApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace CustomerApp;

public partial class App : Application
{
    private readonly IServiceProvider _services;
    private Window? _window;

    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();

        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        services.AddSingleton<ICustomerService, CustomerService>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        services.AddTransient<CustomersViewModel>();
        services.AddTransient<CreateCustomerViewModel>();
        services.AddTransient<SettingsViewModel>();

        services.AddTransient<CustomersPage>();
        services.AddTransient<CreateCustomerPage>();
        services.AddTransient<SettingsPage>();

        _services = services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = _services.GetRequiredService<MainWindow>();

        var navigation = _services.GetRequiredService<INavigationService>();
        navigation.Navigate(AppPage.Customers);

        _window.Activate();
    }
}
