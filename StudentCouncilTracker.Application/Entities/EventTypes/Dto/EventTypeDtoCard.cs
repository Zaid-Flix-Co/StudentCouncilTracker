using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Dto;

public class EventTypeDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}