using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Mappers;

public class AfterMapEventTypeToDtoData : IMappingAction<EventType, EventTypeDtoData>
{
    public void Process(EventType source, EventTypeDtoData destination, ResolutionContext context)
    {

    }
}