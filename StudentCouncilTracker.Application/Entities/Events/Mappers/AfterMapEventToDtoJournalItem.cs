using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;

namespace StudentCouncilTracker.Application.Entities.Events.Mappers;

public class AfterMapEventToDtoJournalItem : IMappingAction<Event, EventDtoJournalItem>
{
    public void Process(Event source, EventDtoJournalItem destination, ResolutionContext context)
    {
        destination.Permissions = new Permission
        {
            Create = true,
            Edit = true,
            Delete = true
        };
    }
}