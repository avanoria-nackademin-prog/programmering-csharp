using System;
using System.IO;

namespace CustomerApp.Models;

public class AppSettings
{
    public string CustomerFilePath { get; set; } = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "CustomerApp",
        "customers.json");
}