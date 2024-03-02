using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Repositories.Bases;
using X.PagedList;

namespace StudentCouncilTracker.Application.Entities.Events.Repositories;

public class EventRepository(IStudentCouncilTrackerDbContext context, IMapper mapper) : EfRepository<Event>((DbContext)context, mapper),
        IEventRepository
{
    #region GETLIST

    public Task<ListDto> GetList(ListFilter filter)
    {
        return _getList(filter, s => mapper.Map<EventDtoCard>(s));
    }

    public async Task<Event> GetCardByIdAsync(long id)
    {
        var events =
            context.Events
                .Include(e => e.EventType)
                .Include(e => e.ResponsibleUser)
                .AsNoTracking();
        var card = await events.FirstOrDefaultAsync(w => w.Id == id);

        return card!;
    }

    private async Task<ListDto> _getList(ListFilter filter, Func<Event, object> selector)
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

    private IQueryable<Event> _getQueue()
    {
        var queue = context.Events;
        return queue.OrderBy(q => q.Name);
    }

    #endregion
}