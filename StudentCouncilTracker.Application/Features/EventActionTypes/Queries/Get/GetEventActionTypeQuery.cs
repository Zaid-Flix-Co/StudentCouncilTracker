using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventActionTypes.Queries.Get;

public record GetEventActionTypeQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetEventActionTypeQueryHandler(IEventActionTypeRepository repository) : IRequestHandler<GetEventActionTypeQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetEventActionTypeQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await repository.GetList(request.Filter));
        return result;
    }
}