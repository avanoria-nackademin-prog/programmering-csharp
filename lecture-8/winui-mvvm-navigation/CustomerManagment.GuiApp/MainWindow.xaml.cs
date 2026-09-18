using CustomerManagment.GuiApp.Navigation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CustomerManagment.GuiApp;

public sealed partial class MainWindow : Window
{
    private readonly INavigationService _navigationService;

    public MainWindow(INavigationService navigationSerivce)
    {
        InitializeComponent();

        _navigationService = navigationSerivce;
        _navigationService.Initialize(ContentFrame);

        _navigationService.Navigate(AppPage.Home);
    }

    private void ContentFrame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        MainNavigation.IsBackEnabled = true;
    }

    private void MainNavigation_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer is not NavigationViewItem item)
            return;

        AppPage? page = item.Tag?.ToString() switch
        {
            "home" => AppPage.Home,
            _ => null,
        };

        if (page is AppPage destination)
            _navigationService.Navigate(destination);
    }

    private void MainNavigation_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
    {
        _navigationService.GoBack();
    }
}
