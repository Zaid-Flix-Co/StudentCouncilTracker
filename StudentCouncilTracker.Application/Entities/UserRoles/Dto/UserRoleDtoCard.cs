using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.UserRoles.Dto;

public class UserRoleDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}