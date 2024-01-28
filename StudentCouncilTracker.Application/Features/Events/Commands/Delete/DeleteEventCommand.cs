using MediatR;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Events.Commands.Delete;

public record DeleteEventCommand(int Id) : IRequest<OperationResult>;

public class DeleteEventCommandHandler(IEventRepository repository) : IRequestHandler<DeleteEventCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult();
        var id = request.Id;
        var stored = await repository.GetByIdAsync(id);

        if (stored == null)
        {
            result.AddError($"Entity with identifier {id} not found");
            return result;
        }

        repository.Delete(stored);

        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        return result;
    }
}
