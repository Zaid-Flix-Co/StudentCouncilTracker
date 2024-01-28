using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventTypes.Queries.GetById;

public record GetEventTypeByIdQuery(int Id) : IRequest<EventTypeDto>;

public class GetEventTypeByIdQueryHandler(IEventTypeRepository repository, IMapper mapper) : IRequestHandler<GetEventTypeByIdQuery, EventTypeDto>
{
    public async Task<EventTypeDto> Handle(GetEventTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<EventTypeDto>(source);
        return ret;
    }
}