using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerApp.Services;
using System;
using System.Threading.Tasks;

namespace CustomerApp.ViewModels;

public partial class SettingsViewModel(ISettingsService settingsService) : ObservableObject
{
    private readonly ISettingsService _settingsService = settingsService;

    [ObservableProperty]
    public partial string CustomerFilePath { get; set; } = settingsService.CustomerFilePath;

    [ObservableProperty]
    public partial string StatusMessage { get; private set; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public partial bool IsBusy { get; private set; }

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async Task SaveAsync()
    {
        IsBusy = true;
        StatusMessage = string.Empty;

        try
        {
            await _settingsService.SetCustomerFilePathAsync(CustomerFilePath);

            CustomerFilePath = _settingsService.CustomerFilePath;
            StatusMessage = "Inställningen har sparats. Öppna Kunder för att läsa den valda filen.";
        }
        catch (ArgumentException ex)
        {
            StatusMessage = ex.Message;
        }
        catch (Exception)
        {
            StatusMessage = "Inställningen kunde inte sparas.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}