using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.UserRoles.Dto.Combobox;

public class UserRoleDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}