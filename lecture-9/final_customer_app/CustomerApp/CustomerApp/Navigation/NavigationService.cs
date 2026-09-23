using CommunityToolkit.Mvvm.ComponentModel;
using CustomerApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CustomerApp.Navigation;

public partial class NavigationService(IServiceProvider services) : ObservableObject, INavigationService
{
    [ObservableProperty]
    public partial Page? CurrentPage { get; private set; }

    public void Navigate(AppPage page)
    {
        CurrentPage = page switch
        {
            AppPage.Customers => services.GetRequiredService<CustomersPage>(),
            AppPage.CreateCustomer => services.GetRequiredService<CreateCustomerPage>(),
            AppPage.Settings => services.GetRequiredService<SettingsPage>(),
            _ => throw new ArgumentOutOfRangeException(nameof(page))
        };
    }
}
