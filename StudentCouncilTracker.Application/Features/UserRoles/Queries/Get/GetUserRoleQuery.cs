using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.UserRoles.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.UserRoles.Queries.Get;

public record GetUserRoleQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetUserRoleQueryHandler(IUserRoleRepository repository) : IRequestHandler<GetUserRoleQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await repository.GetList(request.Filter));
        return result;
    }
}