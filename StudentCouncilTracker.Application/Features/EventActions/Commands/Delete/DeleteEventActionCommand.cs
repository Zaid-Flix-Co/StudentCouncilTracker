using MediatR;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActions.Commands.Delete;

public record DeleteEventActionCommand(int Id) : IRequest<OperationResult>;

public class DeleteEventActionCommandHandler(IEventActionRepository repository) : IRequestHandler<DeleteEventActionCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteEventActionCommand request, CancellationToken cancellationToken)
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
