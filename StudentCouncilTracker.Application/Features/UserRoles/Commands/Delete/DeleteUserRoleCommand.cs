using MediatR;
using StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.UserRoles.Commands.Delete;

public record DeleteUserRoleCommand(int Id) : IRequest<OperationResult>;

public class DeleteUserRoleCommandHandler(IUserRoleRepository repository) : IRequestHandler<DeleteUserRoleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
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
