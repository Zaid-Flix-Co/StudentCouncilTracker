using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventActionTypes.Queries.GetById;

public record GetEventActionTypeByIdQuery(int Id) : IRequest<EventActionTypeDto>;

public class GetEventActionTypeByIdQueryHandler(IEventActionTypeRepository repository, IMapper mapper) : IRequestHandler<GetEventActionTypeByIdQuery, EventActionTypeDto>
{
    public async Task<EventActionTypeDto> Handle(GetEventActionTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<EventActionTypeDto>(source);
        return ret;
    }
}