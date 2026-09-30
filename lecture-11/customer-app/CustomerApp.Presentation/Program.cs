using CustomerApp.Application;
using CustomerApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var customersDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CustomerApp");
var customersFilePath = Path.Combine(customersDirectory, "customers.json");

var services = new ServiceCollection();

services.AddApplication();
services.AddInfrastructure(customersFilePath);

using var serviceProvider = services.BuildServiceProvider();


