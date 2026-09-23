using CommunityToolkit.Mvvm.ComponentModel;
using CustomerApp.Models;
using CustomerApp.Repositories;
using CustomerApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CustomerApp.ViewModels;

public partial class CustomersViewModel(ICustomerService customerService) : ObservableObject
{
    public ObservableCollection<Customer> Customers { get; } = [];

    [ObservableProperty]
    public partial string StatusMessage { get; private set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public partial bool IsBusy { get; private set; }

    public bool IsNotBusy => !IsBusy;

    public async Task LoadCustomersAsync()
    {
        IsBusy = true;

        try
        {
            var customers = await customerService.GetAllCustomersAsync();

            Customers.Clear();

            foreach (var customer in customers)
                Customers.Add(customer);

            StatusMessage = $"{Customers.Count} kunder.";

        }
        catch
        {
            StatusMessage = "Kunder kunde inte läsas in från angiven fil.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
