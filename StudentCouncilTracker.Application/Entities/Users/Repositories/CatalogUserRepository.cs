using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.Repositories.Bases;
using X.PagedList;

namespace StudentCouncilTracker.Application.Entities.Users.Repositories;

public class CatalogUserRepository(IStudentCouncilTrackerDbContext context, IMapper mapper) : EfRepository<CatalogUser>((DbContext)context, mapper),
        ICatalogUserRepository
{
    #region GETLIST

    public Task<ListDto> GetList(ListFilter filter)
    {
        return _getList(filter, s => mapper.Map<CatalogUserDtoCard>(s));
    }

    public async Task<CatalogUser> GetByLogin(CatalogUserDtoData user)
    {
        var users =
            context.CatalogUsers
                .Include(u => u.Role)
                .AsNoTracking();
        var card = await users.FirstOrDefaultAsync(w => w.Email == user.Email!.Value && w.Password == user.Password!.Value);

        return card;
    }

    public async Task<CatalogUser> GetCardByIdAsync(long id)
    {
        var users =
            context.CatalogUsers
                .Include(u => u.Role)
                .AsNoTracking();
        var card = await users.FirstOrDefaultAsync(w => w.Id == id);

        return card;
    }

    private async Task<ListDto> _getList(ListFilter filter, Func<CatalogUser, object> selector)
    {
        var query = _getQueue();

        var filterQuery = filter.query;

        var filterQueries = (filterQuery ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var q in filterQueries)
            query = query.Where(w => EF.Functions.Like((w.Name!).ToLower(), $"%{q}%"));

        var ret = await query.ToPagedListAsync(1, 300);
        var items = ret.Select(selector);

        return new ListDto
        {
            Items = items,
            PageCount = items.PageCount
        };
    }

    private IQueryable<CatalogUser> _getQueue()
    {
        var queue = context.CatalogUsers;
        return queue.OrderBy(q => q.Name);
    }

    #endregion
}