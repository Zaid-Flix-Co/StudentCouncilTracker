using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionStatuses.Queries.GetById;

public record GetEventActionStatusByIdQuery(int Id) : IRequest<OperationResult<EventActionStatusDto>>;

public class GetEventActionStatusByIdQueryHandler(IEventActionStatusRepository repository, IMapper mapper) : IRequestHandler<GetEventActionStatusByIdQuery, OperationResult<EventActionStatusDto>>
{
    public async Task<OperationResult<EventActionStatusDto>> Handle(GetEventActionStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventActionStatusDto>();
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<EventActionStatusDto>(source);
        operationResult.SetValue(ret);
        return ret;
    }
}