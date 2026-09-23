using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerApp.Navigation;
using CustomerApp.Services;
using System;
using System.Threading.Tasks;

namespace CustomerApp.ViewModels;

public partial class CreateCustomerViewModel(ICustomerService customerService, INavigationService navigationService) : ObservableObject
{
    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string EmailAddress { get; set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NameLabel))]
    public partial bool IsCompany { get; set; }

    [ObservableProperty]
    public partial string StatusMessage { get; private set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public partial bool IsBusy { get; private set; }

    public string NameLabel => IsCompany ? "Företagsnamn" : "Kundnamn";

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task SaveAsync()
    {
        IsBusy = true;
        StatusMessage = string.Empty;

        try
        {
            await customerService.CreateAsync(CustomerName, EmailAddress, IsCompany);
        }
        catch (ArgumentException ex)
        {
            StatusMessage = ex.Message;
            return;
        }
        catch (Exception)
        {
            StatusMessage = "Kunden kunde inte sparas. Kontrollera kundfilen och att du kan skriva till mappen.";
            return;
        }
        finally
        {
            IsBusy = false;
        }

        navigationService.Navigate(AppPage.Customers);
    }

    [RelayCommand]
    private void Cancel()
    {
        navigationService.Navigate(AppPage.Customers);
    }
}
