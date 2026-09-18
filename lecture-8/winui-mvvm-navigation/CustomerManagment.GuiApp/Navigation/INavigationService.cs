using Microsoft.UI.Xaml.Controls;

namespace CustomerManagment.GuiApp.Navigation;

public interface INavigationService
{
    bool CanGoBack { get; }

    void Initialize(Frame frame);
    void Navigate(AppPage page, string? parameter = null);
    void GoBack();
}
