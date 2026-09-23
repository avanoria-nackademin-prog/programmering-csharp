using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerApp.Repositories;

public class JsonFileCustomerRepository : ICustomerRepository
{
    private readonly string _filePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        "CustomerApp",
        "customers.json"
    );

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };

    public async Task<List<Customer>> GetAllAsync()
    {

        if (!File.Exists(_filePath))
            return [];

        string json = await File.ReadAllTextAsync(_filePath);

        var customers = JsonSerializer.Deserialize<List<Customer>>(json, _options)
            ?? throw new JsonException("Kundfilen måste innehålla en json-baserad lista");

        return customers;
    }

    public async Task SaveAllAsync(IEnumerable<Customer> customers)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        string json = JsonSerializer.Serialize(customers, _options);
        var tempPath = _filePath + ".tmp";

        await File.WriteAllTextAsync(tempPath, json);
        File.Move(tempPath, _filePath, overwrite: true);
    }
}
