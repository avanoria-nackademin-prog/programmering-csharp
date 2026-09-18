using CustomerManagment.GuiApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;


namespace CustomerManagment.GuiApp.Pages;


public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel { get; }

    public HomePage()
    {
        ViewModel = App.Provider.GetRequiredService<HomeViewModel>();

        InitializeComponent();
    }
}

