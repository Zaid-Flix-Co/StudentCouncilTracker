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
        var isChairman = userProvider.Role == Role.Chairman;

        destination.Permissions = new Permission
        {
            Create = isChairman,
            Edit = isChairman,
            Delete = isChairman
        };
    }
}