﻿using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventTypes.Commands.CreateEventType;

public record CreateEventTypeCommand : IRequest<OperationResult<EventTypeDto>>;

public class CreateEventTypeCommandHandler(IEventTypeRepository repository, IMapper mapper) : IRequestHandler<CreateEventTypeCommand, OperationResult<EventTypeDto>>
{
    public async Task<OperationResult<EventTypeDto>> Handle(CreateEventTypeCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventTypeDto());
        var entity = new EventType
        {
            CreatedDate = DateTime.UtcNow,
            Name = "Новый тип мероприятия"
        };
        repository.Insert(entity);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var eventType = await repository.GetCardByIdAsync(entity.Id);
        var dto = mapper.Map<EventTypeDto>(eventType);
        result.SetValue(dto);

        return result;
    }
}