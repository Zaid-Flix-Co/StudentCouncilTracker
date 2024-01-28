using MediatR;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.Features.Events.Queries.Get;

public record GetEventQuery(ListFilter Filter) : IRequest<IOperationResult<ListDto>>;

public class GetEventQueryHandler(IEventRepository repository) : IRequestHandler<GetEventQuery, IOperationResult<ListDto>>
{
    public async Task<IOperationResult<ListDto>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(await repository.GetList(request.Filter));
        return result;
    }
}