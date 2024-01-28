using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Events.Dto;

public class EventDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}