using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;

public class EventActionType : UserCuBase, IHaveId
{
    public int Id { get; set; }
}