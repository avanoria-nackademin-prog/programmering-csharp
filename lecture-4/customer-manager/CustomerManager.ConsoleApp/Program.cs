using CustomerManager.ConsoleApp.Customers.Dialogs;
using CustomerManager.ConsoleApp.Customers.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ICustomerService, InMemoryCustomerService>();
builder.Services.AddTransient<ICustomerDialog, CustomerDialog>();

using var host = builder.Build();