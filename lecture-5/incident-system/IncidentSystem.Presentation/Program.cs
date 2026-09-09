using IncidentSystem.Application;
using IncidentSystem.Infrastructure;
using IncidentSystem.Presentation.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddTransient<CustomerDialog>();

var app = builder.Build();


var customerDialog = app.Services.GetRequiredService<CustomerDialog>();
customerDialog.CreateCustomerDialog();
customerDialog.ViewCustomersDialog();

