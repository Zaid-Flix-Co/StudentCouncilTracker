using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.Users.Interfaces;

public interface ICatalogUserRepository : IRepository<CatalogUser>
{
    Task<ListDto> GetList(ListFilter filter);

    Task<CatalogUser> GetByLogin(CatalogUserDtoData user);

    Task<CatalogUser> GetCardByIdAsync(long id);
}