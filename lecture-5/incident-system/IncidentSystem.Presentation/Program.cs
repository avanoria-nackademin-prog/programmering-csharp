using IncidentSystem.Infrastructure;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddInfrastructure();

var app = builder.Build();
