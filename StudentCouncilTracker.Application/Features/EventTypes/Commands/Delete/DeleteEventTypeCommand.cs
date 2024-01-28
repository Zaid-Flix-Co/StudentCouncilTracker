using MediatR;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventTypes.Commands.Delete;

public record DeleteEventTypeCommand(int Id) : IRequest<OperationResult>;

public class DeleteEventTypeCommandHandler(IEventTypeRepository repository) : IRequestHandler<DeleteEventTypeCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteEventTypeCommand request, CancellationToken cancellationToken)
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
