﻿using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Events.Queries.GetById;

public record GetEventByIdQuery(int Id, string UserName, Role Role) : IRequest<OperationResult<EventDto>>;

public class GetEventByIdQueryHandler(IEventRepository repository, IMapper mapper) : IRequestHandler<GetEventByIdQuery, OperationResult<EventDto>>
{
    public async Task<OperationResult<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventDto>();
        var source = await repository.GetByIdAsync(request.Id);

        if (source == null)
        {
            operationResult.AddError($"Объект с ID {request.Id} не найден");
            return operationResult;
        }

        var ret = mapper.Map<EventDto>(source);
        operationResult.SetValue(ret);
        return operationResult;
    }
}