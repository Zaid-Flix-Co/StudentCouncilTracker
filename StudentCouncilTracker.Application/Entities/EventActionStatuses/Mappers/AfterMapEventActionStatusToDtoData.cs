using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Mappers;

public class AfterMapEventActionStatusToDtoData : IMappingAction<EventActionStatus, EventActionStatusDtoData>
{
    public void Process(EventActionStatus source, EventActionStatusDtoData destination, ResolutionContext context)
    {

    }
}