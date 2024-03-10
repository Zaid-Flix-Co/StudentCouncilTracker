using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionTypes.Commands.Create;

public record CreateEventActionTypeCommand(string UserName) : IRequest<OperationResult<EventActionTypeDto>>;

public class CreateEventActionTypeCommandHandler(IEventActionTypeRepository repository, IMapper mapper) : IRequestHandler<CreateEventActionTypeCommand, OperationResult<EventActionTypeDto>>
{
    private const string DefaultEventActionTypeName = "Новый тип задачи";

    public async Task<OperationResult<EventActionTypeDto>> Handle(CreateEventActionTypeCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventActionTypeDto());
        var eventActionType = new EventActionType
        {
            CreatedDate = DateTime.UtcNow,
            CreatedUserName = request.UserName,
            Name = DefaultEventActionTypeName
        };
        repository.Insert(eventActionType);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var action = await repository.GetCardByIdAsync(eventActionType.Id);
        var dto = mapper.Map<EventActionTypeDto>(action);
        result.SetValue(dto);

        return result;
    }
}
