using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventTypes.Queries.Get;

public record GetEventTypeQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetEventTypeQueryHandler(IEventTypeRepository repository) : IRequestHandler<GetEventTypeQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetEventTypeQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await repository.GetList(request.Filter));
        return result;
    }
}