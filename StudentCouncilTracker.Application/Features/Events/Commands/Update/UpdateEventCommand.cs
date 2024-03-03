using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Events.Commands.Update;

public record UpdateEventCommand(int Id, EventDtoData Model, string UserName) : IRequest<OperationResult<EventDto>>;

public class UpdateEventCommandHandler(IEventRepository repository, IMapper mapper) : IRequestHandler<UpdateEventCommand, OperationResult<EventDto>>
{
    public async Task<OperationResult<EventDto>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventDto());
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
        var dto = mapper.Map<EventDto>(card);
        result.SetValue(dto);

        return result;
    }
}