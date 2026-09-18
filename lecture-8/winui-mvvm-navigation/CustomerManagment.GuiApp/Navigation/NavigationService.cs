using CustomerManagment.GuiApp.Pages;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CustomerManagment.GuiApp.Navigation;

public class NavigationService : INavigationService
{
    private Frame? _frame;

    public bool CanGoBack => _frame?.CanGoBack == true;

    public void GoBack()
    {
        if (_frame is { CanGoBack: true })
            _frame.GoBack();
    }

    public void Initialize(Frame frame)
    {
        _frame = frame;
    }

    public void Navigate(AppPage page, string? parameter = null)
    {
        var frame = _frame 
            ?? throw new InvalidOperationException("Navigation has not been initialized.");

        var nextPage = page switch
        {
            AppPage.Home => typeof(HomePage),
            _ => throw new ArgumentOutOfRangeException($"Unable to navigate to {page}.")
        };

        if (frame.CurrentSourcePageType == nextPage && parameter is null)
            return;

        if (!frame.Navigate(nextPage, parameter))
            throw new InvalidOperationException($"Unable to navigate to {page}.");
    }
}