using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActions.Queries.GetById;

public record GetEventActionByIdQuery(int Id) : IRequest<OperationResult<EventActionDto>>;

public class GetEventActionByIdQueryHandler(IEventActionRepository repository, IMapper mapper) : IRequestHandler<GetEventActionByIdQuery, OperationResult<EventActionDto>>
{
    public async Task<OperationResult<EventActionDto>> Handle(GetEventActionByIdQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventActionDto>();
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<EventActionDto>(source);
        operationResult.SetValue(ret);
        return operationResult;
    }
}