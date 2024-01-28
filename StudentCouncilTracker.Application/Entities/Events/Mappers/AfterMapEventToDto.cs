using AutoMapper;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;

namespace StudentCouncilTracker.Application.Entities.Events.Mappers;

public class AfterMapEventToDto : IMappingAction<Event, EventDto>
{
    public void Process(Event source, EventDto destination, ResolutionContext context)
    {
    }
}