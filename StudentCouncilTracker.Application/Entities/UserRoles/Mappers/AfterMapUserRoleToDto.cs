using AutoMapper;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;

namespace StudentCouncilTracker.Application.Entities.UserRoles.Mappers;

public class AfterMapUserRoleToDto : IMappingAction<UserRole, UserRoleDto>
{
    public void Process(UserRole source, UserRoleDto destination, ResolutionContext context)
    {

    }
}