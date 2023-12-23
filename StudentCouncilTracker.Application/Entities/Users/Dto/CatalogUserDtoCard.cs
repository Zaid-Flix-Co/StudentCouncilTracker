using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Users.Dto;

public class CatalogUserDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}