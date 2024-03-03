using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionStatuses.Commands.Update;

public record UpdateEventActionStatusCommand(int Id, EventActionStatusDtoData Model, string UserName) : IRequest<OperationResult<EventActionStatusDto>>;

public class UpdateEventActionStatusCommandHandler(IEventActionStatusRepository repository, IMapper mapper) : IRequestHandler<UpdateEventActionStatusCommand, OperationResult<EventActionStatusDto>>
{
    public async Task<OperationResult<EventActionStatusDto>> Handle(UpdateEventActionStatusCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventActionStatusDto());
        var id = request.Id;
        var stored = await repository.GetByIdAsync(id);

        if (stored == null)
        {
            result.AddError($"Entity with identifier {id} not found");
            return result;
        }

        var res = stored.Edit(request.Model, request.UserName);
        if(!res.Ok)
        {
            result.AddReasons(res.Reasons);
            return result;
        }

        repository.Update(stored);

        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var card = await repository.GetCardByIdAsync(id);
        var dto = mapper.Map<EventActionStatusDto>(card);
        result.SetValue(dto);

        return result;
    }
}