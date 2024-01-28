using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActions.Dto;

public class EventActionDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}