using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Commands.Delete;

public record DeleteUserCommand(int Id) : IRequest<OperationResult>;

public class DeleteCatalogUserCommandHandler(ICatalogUserRepository repository) : IRequestHandler<DeleteUserCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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
