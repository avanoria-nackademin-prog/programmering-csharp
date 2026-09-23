using System.Threading.Tasks;

namespace CustomerApp.Services;

public interface ISettingsService
{
    string CustomerFilePath { get; }

    Task SetCustomerFilePathAsync(string filePath);
}