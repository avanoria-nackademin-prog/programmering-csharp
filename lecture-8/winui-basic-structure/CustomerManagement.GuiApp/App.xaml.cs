using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;

namespace CustomerManagement.GuiApp;

public partial class App : Application
{
    private readonly IServiceProvider _provider;
    private Window? _window;

    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();

        services.AddTransient<MainWindow>();

        _provider = services.BuildServiceProvider();

    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = _provider.GetRequiredService<MainWindow>();
        _window.Activate();
    }

}
