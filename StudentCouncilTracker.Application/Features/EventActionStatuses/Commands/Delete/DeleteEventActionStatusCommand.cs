using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionStatuses.Commands.Delete;

public record DeleteEventActionStatusCommand(int Id) : IRequest<OperationResult>;

public class DeleteEventActionStatusCommandHandler(IEventActionStatusRepository repository) : IRequestHandler<DeleteEventActionStatusCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteEventActionStatusCommand request, CancellationToken cancellationToken)
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
