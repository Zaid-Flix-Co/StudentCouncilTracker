using Microsoft.Extensions.DependencyInjection;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.Entities.EventActions.Repositories;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Repositories;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Repositories;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.Entities.Events.Repositories;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;
using StudentCouncilTracker.Application.Entities.EventTypes.Repositories;
using StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Repositories;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.Entities.Users.Repositories;
using StudentCouncilTracker.Application.FileSavers;
using System.Reflection;

namespace StudentCouncilTracker.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        #region REPOSITORIES

        services.AddScoped<ICatalogUserRepository, CatalogUserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventTypeRepository, EventTypeRepository>();
        services.AddScoped<IEventActionRepository, EventActionRepository>();
        services.AddScoped<IEventActionTypeRepository, EventActionTypeRepository>();
        services.AddScoped<IEventActionStatusRepository, EventActionStatusRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        #endregion

        services.AddScoped<FileSaverService>();

        services.AddHttpContextAccessor();

        return services;
    }
}