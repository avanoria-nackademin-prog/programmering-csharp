using CustomerApp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace CustomerApp.Views;


public sealed partial class CustomersPage : Page
{
    public CustomersViewModel ViewModel { get; }

    public CustomersPage(CustomersViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadAsync();
    }
}
