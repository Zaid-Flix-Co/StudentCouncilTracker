using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentCouncilTracker.Persistence.DependencyInjection;

namespace StudentCouncilTracker.Desktop.Admin.DependencyInjection;

public static class DesktopServiceCollectionExtensions
{
    public static IServiceCollection AddDesktop(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }
}