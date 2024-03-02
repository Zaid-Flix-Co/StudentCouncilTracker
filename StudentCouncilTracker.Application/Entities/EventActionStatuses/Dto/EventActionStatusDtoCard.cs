using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;

public class EventActionStatusDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}