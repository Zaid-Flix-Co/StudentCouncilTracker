using AutoMapper;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;

namespace StudentCouncilTracker.Application.Entities.Events.Mappers;

public class AfterMapEventToDtoData : IMappingAction<Event, EventDtoData>
{
    public void Process(Event source, EventDtoData destination, ResolutionContext context)
    {

    }
}