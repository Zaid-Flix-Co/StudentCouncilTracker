using StudentCouncilTracker.Application.Entities.UserRoles.Enums;

namespace StudentCouncilTracker.Application.Services.UserProviders;

public class UserProvider : IUserProvider
{
    public string Name { get; set; }

    public Role Role { get; set; }
}