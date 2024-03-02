using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Repositories.Bases;
using X.PagedList;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Repositories;

public class EventActionStatusRepository(IStudentCouncilTrackerDbContext context, IMapper mapper) : EfRepository<EventActionStatus>((DbContext)context, mapper),
        IEventActionStatusRepository
{
    #region GETLIST

    public Task<ListDto> GetList(ListFilter filter)
    {
        return _getList(filter, s => mapper.Map<EventActionStatusDtoCard>(s));
    }

    public async Task<EventActionStatus> GetCardByIdAsync(long id)
    {
        var eventActionStatuses = context.EventActionStatuses
                .AsNoTracking();
        var card = await eventActionStatuses.FirstOrDefaultAsync(w => w.Id == id);

        return card!;
    }

    private async Task<ListDto> _getList(ListFilter filter, Func<EventActionStatus, object> selector)
    {
        var query = _getQueue();

        var filterQuery = filter.query;

        var filterQueries = (filterQuery ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var q in filterQueries)
            query = query.Where(w => EF.Functions.Like(w.Name.ToLower(), $"%{q}%"));

        var ret = await query.ToPagedListAsync(1, 300);
        var items = ret.Select(selector);

        return new ListDto
        {
            Items = items,
            PageCount = items.PageCount
        };
    }

    private IQueryable<EventActionStatus> _getQueue()
    {
        var queue = context.EventActionStatuses;
        return queue.OrderBy(q => q.Name);
    }

    #endregion
}