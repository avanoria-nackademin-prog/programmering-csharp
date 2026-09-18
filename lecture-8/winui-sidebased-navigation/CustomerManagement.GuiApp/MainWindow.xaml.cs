using CustomerManagement.GuiApp.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;


namespace CustomerManagement.GuiApp;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ContentFrame.Navigate(typeof(HomePage));
    }

    private void ContentFrame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {

    }

    private void MainNavigation_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer is not NavigationViewItem item)
            return;

        Type? nextPage = item.Tag?.ToString() switch
        {
            "home" => typeof(HomePage),
            "customers" => typeof(CustomersPage),
            _ => null
        };

        if (nextPage is null || ContentFrame.CurrentSourcePageType == nextPage)
            return;

        ContentFrame.Navigate(nextPage);

    }

    private void MainNavigation_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
    {
        if (ContentFrame.CanGoBack)
            ContentFrame.GoBack();
    }
}
