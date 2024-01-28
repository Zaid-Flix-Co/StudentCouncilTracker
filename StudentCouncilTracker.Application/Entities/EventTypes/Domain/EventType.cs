using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Domain;

public class EventType : UserCuBase, IHaveId
{
    public int Id { get; set; }

    public required string Name { get; set; }
}