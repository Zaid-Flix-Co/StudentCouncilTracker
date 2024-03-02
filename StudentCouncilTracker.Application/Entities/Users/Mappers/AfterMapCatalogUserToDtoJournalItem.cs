using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;

namespace StudentCouncilTracker.Application.Entities.Users.Mappers;

public class AfterMapCatalogUserToDtoJournalItem : IMappingAction<CatalogUser, CatalogUserDtoJournalItem>
{
    public void Process(CatalogUser source, CatalogUserDtoJournalItem destination, ResolutionContext context)
    {
        destination.Permissions = new Permission
        {
            Create = true,
            Edit = true,
            Delete = true
        };
    }
}