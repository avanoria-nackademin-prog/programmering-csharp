using System;
using System.Text.Json.Serialization;

namespace CustomerApp.Models;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsCompany { get; set; }

    [JsonIgnore]
    public string CustomerType => IsCompany ? "Företag" : "Privatkund";
}
