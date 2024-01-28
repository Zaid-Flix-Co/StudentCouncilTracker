using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventActions.Queries.Get;

public record GetEventActionQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetEventActionQueryHandler(IEventActionRepository repository) : IRequestHandler<GetEventActionQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetEventActionQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await repository.GetList(request.Filter));
        return result;
    }
}