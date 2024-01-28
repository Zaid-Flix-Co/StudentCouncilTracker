using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;

public interface IEventTypeRepository : IRepository<EventType>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<EventType> GetCardByIdAsync(long id);
}