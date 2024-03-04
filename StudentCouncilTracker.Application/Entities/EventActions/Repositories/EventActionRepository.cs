using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Repositories.Bases;
using X.PagedList;

namespace StudentCouncilTracker.Application.Entities.EventActions.Repositories;

public class EventActionRepository(IStudentCouncilTrackerDbContext context, IMapper mapper) : EfRepository<EventAction>((DbContext)context, mapper),
        IEventActionRepository
{
    #region GETLIST

    public Task<ListDto> GetList(ListFilter filter)
    {
        return _getList(filter, s => mapper.Map<EventActionDtoCard>(s));
    }

    public async Task<EventAction> GetCardByIdAsync(long id)
    {
        var eventActions = context.EventActions
            .Include(e => e.Event)
            .Include(e => e.Status)
            .Include(e => e.ResponsibleManager)
            .Include(e => e.EventActionType)
            .AsNoTracking();
        var card = await eventActions.FirstOrDefaultAsync(w => w.Id == id);

        return card!;
    }

    private async Task<ListDto> _getList(ListFilter filter, Func<EventAction, object> selector)
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

    private IQueryable<EventAction> _getQueue()
    {
        var queue = context.EventActions;
        return queue.OrderBy(q => q.Name);
    }

    #endregion
}