using IncidentManagement.Application;
using IncidentManagement.ConsoleApp.Dialogs;
using IncidentManagement.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddInfrastructure();
services.AddApplication();
services.AddDialogs();

using var serviceProvider = services.BuildServiceProvider();
var appDialog = serviceProvider.GetRequiredService<IAppDialog>();

appDialog.StartApplication();
appDialog.CloseApplication();
