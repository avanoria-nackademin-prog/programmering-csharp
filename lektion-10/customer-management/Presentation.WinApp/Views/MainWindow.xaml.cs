using Microsoft.UI.Xaml;
using Presentation.Resources.ViewModels;


namespace Presentation.WinApp.Views;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
