using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Mappers;

public class AfterMapEventActionTypeToDto : IMappingAction<EventActionType, EventActionTypeDto>
{
    public void Process(EventActionType source, EventActionTypeDto destination, ResolutionContext context)
    {

    }
}