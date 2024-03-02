using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventActionStatuses.Queries.Get;

public record GetEventActionStatusQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetEventActionStatusQueryHandler(IEventActionStatusRepository repository) : IRequestHandler<GetEventActionStatusQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetEventActionStatusQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await repository.GetList(request.Filter));
        return result;
    }
}