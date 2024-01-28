using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActions.Mappers;

public class AfterMapEventActionToDto : IMappingAction<EventAction, EventActionDto>
{
    public void Process(EventAction source, EventActionDto destination, ResolutionContext context)
    {

    }
}