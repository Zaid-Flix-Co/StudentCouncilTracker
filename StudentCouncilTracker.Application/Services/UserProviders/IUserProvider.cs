using StudentCouncilTracker.Application.Entities.UserRoles.Enums;

namespace StudentCouncilTracker.Application.Services.UserProviders;

public interface IUserProvider
{
    string Name { get; set; }

    Role Role { get; set; }
}