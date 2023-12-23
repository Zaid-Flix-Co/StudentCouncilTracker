using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.Entities.Users.Repositories;
using StudentCouncilTracker.Application.FileSavers;

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

        #endregion

        services.AddScoped<FileSaverService>();

        services.AddHttpContextAccessor();

        return services;
    }
}