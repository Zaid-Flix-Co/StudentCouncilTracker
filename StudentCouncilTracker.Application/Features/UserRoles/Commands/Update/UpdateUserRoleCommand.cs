using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.UserRoles.Commands.Update;

public record UpdateUserRoleCommand(int Id, UserRoleDtoData Model, string UserName) : IRequest<OperationResult<UserRoleDto>>;

public class UpdateUserRoleCommandHandler(IUserRoleRepository repository, IMapper mapper) : IRequestHandler<UpdateUserRoleCommand, OperationResult<UserRoleDto>>
{
    public async Task<OperationResult<UserRoleDto>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new UserRoleDto());
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
        var dto = mapper.Map<UserRoleDto>(card);
        result.SetValue(dto);

        return result;
    }
}