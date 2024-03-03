using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.UserRoles.Queries.GetById;

public record GetUserRoleByIdQuery(int Id) : IRequest<OperationResult<UserRoleDto>>;

public class GetUserRoleByIdQueryHandler(IUserRoleRepository repository, IMapper mapper) : IRequestHandler<GetUserRoleByIdQuery, OperationResult<UserRoleDto>>
{
    public async Task<OperationResult<UserRoleDto>> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<UserRoleDto>();
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<UserRoleDto>(source);
        operationResult.SetValue(ret);
        return ret;
    }
}