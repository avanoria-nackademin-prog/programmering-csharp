using IncidentManagement.Application;
using IncidentManagement.Infrastructure;
using IncidentManagement.Infrastructure.Persistence;
using IncidentManagement.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace IncidentManagement.UI
{

    public partial class App : Microsoft.UI.Xaml.Application
    {
        private readonly ServiceProvider services;
        private Window? _window;

        public App()
        {
            InitializeComponent();

            services = new ServiceCollection()
                .AddApplication()
                .AddInfrastructure()
                .AddTransient<MainViewModel>()
                .AddTransient<MainWindow>()
                .BuildServiceProvider();
        }

        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var context = services.GetRequiredService<DataContext>();
            await context.Database.EnsureCreatedAsync();

            _window = services.GetRequiredService<MainWindow>();
            _window.Activate();
        }
    }
}
