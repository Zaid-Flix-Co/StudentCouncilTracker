using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;

public interface IEventActionTypeRepository : IRepository<EventActionType>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<EventActionType> GetCardByIdAsync(long id);
}