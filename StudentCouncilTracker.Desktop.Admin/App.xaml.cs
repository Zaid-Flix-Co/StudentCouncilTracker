using Microsoft.Extensions.DependencyInjection;
using StudentCouncilTracker.Desktop.Admin.DependencyInjection;
using System.Windows;
using Microsoft.Extensions.Configuration;
using StudentCouncilTracker.Desktop.Admin.Windows;
using StudentCouncilTracker.Desktop.Admin.Windows.Roles;
using StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;
using StudentCouncilTracker.Desktop.Admin.Windows.Users;

namespace StudentCouncilTracker.Desktop.Admin;

public partial class App : System.Windows.Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        ConfigureServices(services, configuration);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDesktop(configuration);

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