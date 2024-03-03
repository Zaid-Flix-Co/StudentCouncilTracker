using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventTypes.Commands.Update;

public record UpdateEventTypeCommand(int Id, EventTypeDtoData Model, string UserName) : IRequest<OperationResult<EventTypeDto>>;

public class UpdateEventTypeCommandHandler(IEventTypeRepository repository, IMapper mapper) : IRequestHandler<UpdateEventTypeCommand, OperationResult<EventTypeDto>>
{
    public async Task<OperationResult<EventTypeDto>> Handle(UpdateEventTypeCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventTypeDto());
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
        var dto = mapper.Map<EventTypeDto>(card);
        result.SetValue(dto);

        return result;
    }
}