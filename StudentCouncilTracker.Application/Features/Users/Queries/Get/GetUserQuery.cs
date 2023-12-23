using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.Users.Queries.Get;

public record GetUserQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetUserQueryHandler(ICatalogUserRepository catalogUserRepository) : IRequestHandler<GetUserQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await catalogUserRepository.GetList(request.Filter));
        return result;
    }
}