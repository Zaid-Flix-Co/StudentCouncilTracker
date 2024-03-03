using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionStatuses.Commands.Create;

public record CreateEventActionStatusCommand(string UserName) : IRequest<OperationResult<EventActionStatusDto>>;

public class CreateEventActionStatusCommandHandler(IEventActionStatusRepository repository, IMapper mapper) : IRequestHandler<CreateEventActionStatusCommand, OperationResult<EventActionStatusDto>>
{
    public async Task<OperationResult<EventActionStatusDto>> Handle(CreateEventActionStatusCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventActionStatusDto());
        var eventActionStatus = new EventActionStatus
        {
            CreatedDate = DateTime.UtcNow,
            CreatedUserName = request.UserName,
            Name = "Новый тип задачи"
        };
        repository.Insert(eventActionStatus);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var action = await repository.GetCardByIdAsync(eventActionStatus.Id);
        var dto = mapper.Map<EventActionStatusDto>(action);
        result.SetValue(dto);

        return result;
    }
}
