using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActions.Mappers;

public class AfterMapEventActionToDtoJournalItem : IMappingAction<EventAction, EventActionDtoJournalItem>
{
    public void Process(EventAction source, EventActionDtoJournalItem destination, ResolutionContext context)
    {
        destination.Permissions = new Permission
        {
            Create = true,
            Edit = true,
            Delete = true
        };
    }
}