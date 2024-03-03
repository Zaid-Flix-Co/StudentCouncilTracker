using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.UserRoles.Commands.Create;

public record CreateUserRoleCommand(string UserName) : IRequest<OperationResult<UserRoleDto>>;

public class CreateUserRoleCommandHandler(IUserRoleRepository repository, IMapper mapper) : IRequestHandler<CreateUserRoleCommand, OperationResult<UserRoleDto>>
{
    public async Task<OperationResult<UserRoleDto>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new UserRoleDto());
        var userRole = new UserRole
        {
            CreatedDate = DateTime.UtcNow,
            CreatedUserName = request.UserName,
            Name = "Новый тип задачи"
        };
        repository.Insert(userRole);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var action = await repository.GetCardByIdAsync(userRole.Id);
        var dto = mapper.Map<UserRoleDto>(action);
        result.SetValue(dto);

        return result;
    }
}
