using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.Users.Interfaces;

public interface ICatalogUserRepository : IRepository<CatalogUser>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<CatalogUser> GetCardByIdAsync(long id);
}