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

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        services.AddSingleton<ICustomerRepository, JsonFileCustomerRepository>();
        services.AddSingleton<ICustomerService, CustomerService>();


        services.AddTransient<CustomersViewModel>();
        services.AddTransient<CustomersPage>();

        services.AddTransient<CreateCustomerViewModel>();
        services.AddTransient<CreateCustomerPage>();

        _services = services.BuildServiceProvider();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var navigation = _services.GetRequiredService<INavigationService>();
        navigation.Navigate(AppPage.Customers);

        _window = _services.GetRequiredService<MainWindow>();
        _window.Activate();
    }
}
