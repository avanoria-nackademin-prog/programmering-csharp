using CommunityToolkit.Mvvm.ComponentModel;
using CustomerApp.Navigation;

namespace CustomerApp.ViewModels;

public partial class MainViewModel(INavigationService navigationService) : ObservableObject
{
    public INavigationService Navigation { get; } = navigationService;
}
