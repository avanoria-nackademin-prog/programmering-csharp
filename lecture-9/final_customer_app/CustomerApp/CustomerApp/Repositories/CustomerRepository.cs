using CustomerApp.Models;
using CustomerApp.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerApp.Repositories;

public class CustomerRepository(ISettingsService settingsService) : ICustomerRepository
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<Customer>> GetAllAsync()
    {
        var filePath = settingsService.CustomerFilePath;

        if (!File.Exists(filePath))
            return [];

        var json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<List<Customer>>(json, _options)
            ?? throw new JsonException("Kundfilen måste innehålla en lista.");
    }

    public async Task SaveAllAsync(IEnumerable<Customer> customers)
    {
        var filePath = settingsService.CustomerFilePath;

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        var json = JsonSerializer.Serialize(customers, _options);
        var temporaryPath = filePath + ".tmp";

        await File.WriteAllTextAsync(temporaryPath, json);
        File.Move(temporaryPath, filePath, overwrite: true);
    }
}
