using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerApp.Navigation;

namespace CustomerApp.ViewModels;

public partial class MainViewModel(INavigationService navigationService) : ObservableObject
{
    public INavigationService Navigation { get; } = navigationService;

    [RelayCommand]
    private void ShowCustomers()
    {
        Navigation.Navigate(AppPage.Customers);
    }

    [RelayCommand]
    private void ShowSettings()
    {
        Navigation.Navigate(AppPage.Settings);
    }
}