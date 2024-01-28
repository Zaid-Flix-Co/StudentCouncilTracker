using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.EventActions.Interfaces;

public interface IEventActionRepository : IRepository<EventAction>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<EventAction> GetCardByIdAsync(long id);
}