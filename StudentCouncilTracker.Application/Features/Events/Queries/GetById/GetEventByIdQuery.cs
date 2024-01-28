using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;

namespace StudentCouncilTracker.Application.Features.Events.Queries.GetById;

public record GetEventByIdQuery(int Id) : IRequest<EventDto>;

public class GetEventByIdQueryHandler(IEventRepository repository, IMapper mapper) : IRequestHandler<GetEventByIdQuery, EventDto>
{
    public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<EventDto>(source);
        return ret;
    }
}