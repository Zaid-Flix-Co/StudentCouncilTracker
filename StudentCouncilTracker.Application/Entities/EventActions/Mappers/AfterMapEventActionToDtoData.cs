using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActions.Mappers;

public class AfterMapEventActionToDtoData : IMappingAction<EventAction, EventActionDtoData>
{
    public void Process(EventAction source, EventActionDtoData destination, ResolutionContext context)
    {

    }
}