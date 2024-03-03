using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Repositories.Bases;

namespace StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<ListDto> GetList(ListFilter filter);

    public Task<UserRole> GetCardByIdAsync(long id);
}