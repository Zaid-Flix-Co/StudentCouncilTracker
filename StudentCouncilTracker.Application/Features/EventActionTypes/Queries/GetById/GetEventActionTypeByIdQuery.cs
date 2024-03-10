using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActionTypes.Queries.GetById;

public record GetEventActionTypeByIdQuery(int Id) : IRequest<OperationResult<EventActionTypeDto>>;

public class GetEventActionTypeByIdQueryHandler(IEventActionTypeRepository repository, IMapper mapper) : IRequestHandler<GetEventActionTypeByIdQuery, OperationResult<EventActionTypeDto>>
{
    public async Task<OperationResult<EventActionTypeDto>> Handle(GetEventActionTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventActionTypeDto>();
        var source = await repository.GetByIdAsync(request.Id);

        if (source == null)
        {
            operationResult.AddError($"Объект с ID {request.Id} не найден");
            return operationResult;
        }

        var ret = mapper.Map<EventActionTypeDto>(source);
        operationResult.SetValue(ret);

        return operationResult;
    }
}