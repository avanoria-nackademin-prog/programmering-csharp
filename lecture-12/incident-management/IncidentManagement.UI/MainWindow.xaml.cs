using IncidentManagement.UI.ViewModels;
using Microsoft.UI.Xaml;

namespace IncidentManagement.UI;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

}
