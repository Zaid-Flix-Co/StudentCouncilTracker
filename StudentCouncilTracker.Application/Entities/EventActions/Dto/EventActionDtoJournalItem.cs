using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActions.Dto;

public class EventActionDtoJournalItem : IHaveId
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? ResponsibleManager { get; set; }

    public DateTime DeadlineCompletion { get; set; }

    public string EventActionType { get; set; }

    public string? Status { get; set; }

    public Permission Permissions { get; set; }
}