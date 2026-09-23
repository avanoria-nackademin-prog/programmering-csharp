using CustomerApp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace CustomerApp;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        ViewModel.Navigation.PropertyChanged += Navigation_PropertyChanged;
        Closed += Window_Closed;

    }

    private void AppNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {

    }


    private void Navigation_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    {

    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        ViewModel.Navigation.PropertyChanged -= Navigation_PropertyChanged;
        Closed -= Window_Closed;
    }
}
