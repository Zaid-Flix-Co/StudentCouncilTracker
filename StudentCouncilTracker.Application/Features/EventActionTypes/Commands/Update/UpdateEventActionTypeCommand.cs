using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionTypes.Commands.Update;

public record UpdateEventActionTypeCommand(int Id, EventActionTypeDtoData Model, string UserName) : IRequest<OperationResult<EventActionTypeDto>>;

public class UpdateEventActionTypeCommandHandler(IEventActionTypeRepository repository, IMapper mapper) : IRequestHandler<UpdateEventActionTypeCommand, OperationResult<EventActionTypeDto>>
{
    public async Task<OperationResult<EventActionTypeDto>> Handle(UpdateEventActionTypeCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventActionTypeDto());
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
        var dto = mapper.Map<EventActionTypeDto>(card);
        result.SetValue(dto);

        return result;
    }
}