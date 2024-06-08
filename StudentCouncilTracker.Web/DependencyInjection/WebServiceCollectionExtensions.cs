using StudentCouncilTracker.Application.Services.Email;
using StudentCouncilTracker.Web.Services.Catalogs.Auth;
using StudentCouncilTracker.Web.Services.Catalogs.EventActions;
using StudentCouncilTracker.Web.Services.Catalogs.EventActionStatuses;
using StudentCouncilTracker.Web.Services.Catalogs.EventActionTypes;
using StudentCouncilTracker.Web.Services.Catalogs.Events;
using StudentCouncilTracker.Web.Services.Catalogs.EventTypes;
using StudentCouncilTracker.Web.Services.Catalogs.UserRoles;
using StudentCouncilTracker.Web.Services.Catalogs.Users;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.DependencyInjection;

public static class WebServiceCollectionExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
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
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}