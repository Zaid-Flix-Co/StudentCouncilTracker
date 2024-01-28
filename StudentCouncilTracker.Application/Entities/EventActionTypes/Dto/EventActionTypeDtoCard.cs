using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;

public class EventActionTypeDtoCard : IHaveId<int?>
{
    public int? Id { get; set; }

    public string Name { get; set; }
}