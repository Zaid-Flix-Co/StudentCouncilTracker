using Microsoft.Extensions.DependencyInjection;
using StudentCouncilTracker.Desktop.Admin.DependencyInjection;
using System.Windows;
using StudentCouncilTracker.Desktop.Admin.Windows;

namespace StudentCouncilTracker.Desktop.Admin;

public partial class App : System.Windows.Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDesktop();

        services.AddHttpClient("StudentCouncilTrackerWebApi", client =>
        {
            #if (DEBUG)
            client.BaseAddress = new Uri("https://localhost:7090");
            #elif (RELEASE)
            client.BaseAddress = new Uri("https://sandme.ru/api-student-council-tracker/");
            #endif
            client.Timeout = TimeSpan.FromMinutes(15);
        });

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MenuWindow>();
        services.AddSingleton<EventTypeWindow>();
        services.AddSingleton<RoleWindow>();
        services.AddSingleton<TaskTypeWindow>();
        services.AddSingleton<UserWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}