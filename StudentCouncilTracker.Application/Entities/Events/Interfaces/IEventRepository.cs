using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.Events.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<Event> GetCardByIdAsync(long id);
}