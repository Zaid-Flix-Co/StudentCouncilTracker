using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Mappers;

public class AfterMapEventActionTypeToDtoData : IMappingAction<EventActionType, EventActionTypeDtoData>
{
    public void Process(EventActionType source, EventActionTypeDtoData destination, ResolutionContext context)
    {

    }
}