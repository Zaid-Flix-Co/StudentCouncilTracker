using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Users.Dto;

public class CatalogUserDtoJournalItem : IHaveId
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }

    public Permission Permissions { get; set; }
}