using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Mappers;

public class AfterMapEventActionStatusToDto : IMappingAction<EventActionStatus, EventActionStatusDto>
{
    public void Process(EventActionStatus source, EventActionStatusDto destination, ResolutionContext context)
    {

    }
}