using CustomerApp.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace CustomerApp.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel { get; }

    public SettingsPage(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}
