using CustomerApp.Navigation;
using CustomerApp.ViewModels;
using CustomerApp.Views;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using Windows.Graphics;

namespace CustomerApp;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        //AppWindow.Resize(new SizeInt32(1280, 960));
        CenterWindow(1280, 960);
        ViewModel.Navigation.PropertyChanged += Navigation_PropertyChanged;
        Closed += MainWindow_Closed;

        UpdateSelectedItem();
    }

    private void AppNavigation_ItemInvoked(
        NavigationView sender,
        NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            ViewModel.ShowSettingsCommand.Execute(null);
            return;
        }

        if (args.InvokedItemContainer?.Tag is string tag && tag == "customers")
            ViewModel.ShowCustomersCommand.Execute(null);
    }

    private void Navigation_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(INavigationService.CurrentPage))
            UpdateSelectedItem();
    }

    private void UpdateSelectedItem()
    {
        AppNavigation.SelectedItem = ViewModel.Navigation.CurrentPage is SettingsPage
            ? AppNavigation.SettingsItem
            : CustomersNavigationItem;
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        ViewModel.Navigation.PropertyChanged -= Navigation_PropertyChanged;
        Closed -= MainWindow_Closed;
    }

    private void CenterWindow(int width, int height)
    {
        var displayArea = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Nearest);
        var workArea = displayArea.WorkArea;

        width = Math.Min(width, workArea.Width);
        height = Math.Min(height, workArea.Height);

        var x = workArea.X + (workArea.Width - width) / 2;
        var y = workArea.Y + (workArea.Height - height) / 2;

        AppWindow.MoveAndResize(new RectInt32(x, y, width, height), displayArea);
    }
}
