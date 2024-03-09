using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.Application.Entities.EventActions.Mappers;

public class AfterMapEventActionToDtoJournalItem(IUserProvider userProvider) : IMappingAction<EventAction, EventActionDtoJournalItem>
{
    public void Process(EventAction source, EventActionDtoJournalItem destination, ResolutionContext context)
    {
        var isResponsibleManager = userProvider.Name == source.ResponsibleManager?.Name;
        var isResponsibleUser = userProvider.Name == source.Event.ResponsibleUser?.Name;
        var isChairman = userProvider.Role == Role.Chairman;

        destination.Permissions = new Permission
        {
            Create = isChairman,
            Edit = isChairman || isResponsibleManager || isResponsibleUser,
            Delete = isChairman
        };
    }
}