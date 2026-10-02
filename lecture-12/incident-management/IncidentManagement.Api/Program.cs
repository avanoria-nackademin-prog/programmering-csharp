using IncidentManagement.Api.Incidents.Endpoints;
using IncidentManagement.Application;
using IncidentManagement.Infrastructure;
using IncidentManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Database.EnsureCreatedAsync();
}

app.MapIncidentEndpoints();

app.Run();