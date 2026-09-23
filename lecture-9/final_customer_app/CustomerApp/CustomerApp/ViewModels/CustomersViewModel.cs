using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerApp.Models;
using CustomerApp.Navigation;
using CustomerApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CustomerApp.ViewModels;

public partial class CustomersViewModel(ICustomerService customerService, INavigationService navigationService) : ObservableObject
{
    public ObservableCollection<Customer> Customers { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCustomerCommand))]
    public partial Customer? SelectedCustomer { get; set; }

    [ObservableProperty]
    public partial string StatusMessage { get; private set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public partial bool IsBusy { get; private set; }

    public bool IsNotBusy => !IsBusy;

    public async Task LoadAsync()
    {
        IsBusy = true;

        try
        {
            var customers = await customerService.GetAllAsync();

            Customers.Clear();

            foreach (var customer in customers)
                Customers.Add(customer);

            StatusMessage = $"{Customers.Count} kunder.";
        }
        catch (Exception)
        {
            StatusMessage = "Kunderna kunde inte läsas in. Kontrollera filen och sökvägen i inställningarna.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void OpenCreateCustomer()
    {
        navigationService.Navigate(AppPage.CreateCustomer);
    }

    private bool CanDeleteCustomer() => SelectedCustomer is not null;

    [RelayCommand(CanExecute = nameof(CanDeleteCustomer))]
    private async Task DeleteCustomerAsync()
    {
        if (SelectedCustomer is not { } customer)
            return;

        IsBusy = true;

        try
        {
            await customerService.DeleteAsync(customer.Id);

            Customers.Remove(customer);
            SelectedCustomer = null;

            StatusMessage = "Kunden har tagits bort.";
        }
        catch (Exception)
        {
            StatusMessage = "Kunden kunde inte tas bort.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
