using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Dto;

public class EventTypeDtoDataAdmin : EntityDtoData, IHaveId
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}