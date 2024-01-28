using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Repositories.Bases;
using X.PagedList;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Repositories;

public class EventTypeRepository(IStudentCouncilTrackerDbContext context, IMapper mapper) : EfRepository<EventType>((DbContext)context, mapper),
        IEventTypeRepository
{
    #region GETLIST

    public Task<ListDto> GetList(ListFilter filter)
    {
        return _getList(filter, s => mapper.Map<EventTypeDtoCard>(s));
    }

    public async Task<EventType> GetCardByIdAsync(long id)
    {
        var eventTypes =
            context.EventTypes
                .AsNoTracking();
        var card = await eventTypes.FirstOrDefaultAsync(w => w.Id == id);

        return card!;
    }

    private async Task<ListDto> _getList(ListFilter filter, Func<EventType, object> selector)
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

    private IQueryable<EventType> _getQueue()
    {
        var queue = context.EventTypes;
        return queue.OrderBy(q => q.Name);
    }

    #endregion
}