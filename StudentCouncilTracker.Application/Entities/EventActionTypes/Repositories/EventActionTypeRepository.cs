using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Repositories.Bases;
using X.PagedList;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Repositories;

public class EventActionTypeRepository(IStudentCouncilTrackerDbContext context, IMapper mapper) : EfRepository<EventActionType>((DbContext)context, mapper),
        IEventActionTypeRepository
{
    #region GETLIST

    public Task<ListDto> GetList(ListFilter filter)
    {
        return _getList(filter, s => mapper.Map<EventActionTypeDtoCard>(s));
    }

    public async Task<EventActionType> GetCardByIdAsync(long id)
    {
        var eventActionTypes = context.EventActionTypes
                .AsNoTracking();
        var card = await eventActionTypes.FirstOrDefaultAsync(w => w.Id == id);

        return card!;
    }

    private async Task<ListDto> _getList(ListFilter filter, Func<EventActionType, object> selector)
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

    private IQueryable<EventActionType> _getQueue()
    {
        var queue = context.EventActionTypes;
        return queue.OrderBy(q => q.Name);
    }

    #endregion
}