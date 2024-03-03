using AutoMapper;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;

namespace StudentCouncilTracker.Application.Entities.UserRoles.Mappers;

public class AfterMapUserRoleToDtoData : IMappingAction<UserRole, UserRoleDtoData>
{
    public void Process(UserRole source, UserRoleDtoData destination, ResolutionContext context)
    {

    }
}