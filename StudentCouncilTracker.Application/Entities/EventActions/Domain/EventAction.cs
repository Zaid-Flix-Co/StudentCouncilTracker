using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActions.Domain;

public class EventAction : UserCuBase, IHaveId
{
    public int Id { get; set; }
}