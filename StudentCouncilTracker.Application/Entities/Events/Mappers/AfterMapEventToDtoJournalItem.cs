using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.Application.Entities.Events.Mappers;

public class AfterMapEventToDtoJournalItem(IUserProvider userProvider) : IMappingAction<Event, EventDtoJournalItem>
{
    public void Process(Event source, EventDtoJournalItem destination, ResolutionContext context)
    {
        var isResponsibleUser = userProvider.Name == source.ResponsibleUser?.Name;
        var isChairman = userProvider.Role == Role.Chairman;

        destination.Permissions = new Permission
        {
            Create = isChairman,
            Edit = isChairman || isResponsibleUser,
            Delete = isChairman
        };
    }
}