using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionTypes.Commands.Delete;

public record DeleteEventActionTypeCommand(int Id) : IRequest<OperationResult>;

public class DeleteEventActionTypeCommandHandler(IEventActionTypeRepository repository) : IRequestHandler<DeleteEventActionTypeCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteEventActionTypeCommand request, CancellationToken cancellationToken)
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
