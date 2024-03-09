using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.Application.Entities.Users.Mappers;

public class AfterMapCatalogUserToDtoJournalItem(IUserProvider userProvider) : IMappingAction<CatalogUser, CatalogUserDtoJournalItem>
{
    public void Process(CatalogUser source, CatalogUserDtoJournalItem destination, ResolutionContext context)
    {
        destination.Permissions = new Permission
        {
            Create = userProvider.Role == Role.Chairman,
            Edit = userProvider.Role == Role.Chairman,
            Delete = userProvider.Role == Role.Chairman
        };
    }
}