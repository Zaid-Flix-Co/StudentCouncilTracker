using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;

public interface IEventActionStatusRepository : IRepository<EventActionStatus>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<EventActionStatus> GetCardByIdAsync(long id);
}