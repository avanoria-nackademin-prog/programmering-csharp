using CustomerApp.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerApp.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "CustomerApp",
        "settings.json");

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    private AppSettings _settings;

    public string CustomerFilePath => _settings.CustomerFilePath;

    public SettingsService()
    {
        if (File.Exists(_settingsPath))
        {
            var json = File.ReadAllText(_settingsPath);

            _settings = JsonSerializer.Deserialize<AppSettings>(json, _options)
                ?? throw new JsonException("Inställningsfilen innehåller inga inställningar.");
        }
        else
        {
            _settings = new AppSettings();
        }
    }

    public async Task SetCustomerFilePathAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Ange en sökväg.");

        var path = filePath.Trim();

        if (!Path.IsPathFullyQualified(path))
            throw new ArgumentException("Ange en fullständig sökväg, exempelvis C:\\CustomerData\\customers.json.");

        path = Path.GetFullPath(path);

        if (!string.Equals(Path.GetExtension(path), ".json", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Filen måste ha filändelsen .json.");

        var settings = new AppSettings
        {
            CustomerFilePath = path
        };

        Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);

        var json = JsonSerializer.Serialize(settings, _options);
        var temporaryPath = _settingsPath + ".tmp";

        await File.WriteAllTextAsync(temporaryPath, json);
        File.Move(temporaryPath, _settingsPath, overwrite: true);

        _settings = settings;
    }
}