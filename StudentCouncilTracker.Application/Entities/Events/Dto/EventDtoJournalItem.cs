using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Events.Dto;

public class EventDtoJournalItem : IHaveId
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public Permission Permissions { get; set; }
}