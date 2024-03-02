using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Mappers;

public class AfterMapEventTypeToDto : IMappingAction<EventType, EventTypeDto>
{
    public void Process(EventType source, EventTypeDto destination, ResolutionContext context)
    {
    }
}