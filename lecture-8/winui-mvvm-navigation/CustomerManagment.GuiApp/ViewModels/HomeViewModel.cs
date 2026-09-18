using CommunityToolkit.Mvvm.ComponentModel;
using CustomerManagment.GuiApp.Navigation;
using System.Collections.ObjectModel;

namespace CustomerManagment.GuiApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public string Title { get; set; } = "Home Page";

    public ObservableCollection<Customer> Customers { get; set; } = [];


    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
       
        PopulateCustomers();
    }


    public void PopulateCustomers()
    {
        Customers = 
            [
                new Customer { Name = "Avanoria AB" },
                new Customer { Name = "Nackademin AB" },
            ];
    }
        
}

public class Customer
{
    public string Name { get; set; } = null!;
}