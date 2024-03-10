using Microsoft.Extensions.DependencyInjection;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Auth;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventActions;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventActionStatuses;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventActionTypes;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Events;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventTypes;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.UserRoles;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Users;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin.DependencyInjection;

public static class DesktopServiceCollectionExtensions
{
    public static IServiceCollection AddDesktop(this IServiceCollection services)
    {
        services.AddScoped<EventService>();
        services.AddScoped<EventActionService>();
        services.AddScoped<EventTypeService>();
        services.AddScoped<EventActionTypeService>();
        services.AddScoped<EventActionStatusService>();
        services.AddScoped<CatalogUserService>();
        services.AddScoped<AuthService>();
        services.AddScoped<UserRoleService>();
        services.AddScoped<IUserProvider, UserProvider>();

        return services;
    }
}