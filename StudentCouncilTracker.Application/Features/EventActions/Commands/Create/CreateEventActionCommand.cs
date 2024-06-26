﻿using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Enums;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.Services.Email;

namespace StudentCouncilTracker.Application.Features.EventActions.Commands.Create;

public record CreateEventActionCommand(int EventId, string UserName) : IRequest<OperationResult<EventActionDto>>;

public class CreateEventActionCommandHandler(IEventActionRepository repository, IMapper mapper) : IRequestHandler<CreateEventActionCommand, OperationResult<EventActionDto>>
{
    private const string DefaultEventActionName = "Новое задание";

    public async Task<OperationResult<EventActionDto>> Handle(CreateEventActionCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventActionDto());
        var eventAction = new EventAction
        {
            CreatedDate = DateTime.UtcNow,
            CreatedUserName = request.UserName,
            Name = DefaultEventActionName,
            EventId = request.EventId,
            StatusId = (int)ActionStatus.AwaitingExecution,
            DeadlineCompletion = DateTime.UtcNow
        };
        repository.Insert(eventAction);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var action = await repository.GetCardByIdAsync(eventAction.Id);
        var dto = mapper.Map<EventActionDto>(action);
        result.SetValue(dto);

        return result;
    }
}
