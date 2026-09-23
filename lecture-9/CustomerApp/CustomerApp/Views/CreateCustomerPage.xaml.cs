using CustomerApp.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace CustomerApp.Views;

public sealed partial class CreateCustomerPage : Page
{
    public CreateCustomerViewModel ViewModel { get; }

    public CreateCustomerPage(CreateCustomerViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}
