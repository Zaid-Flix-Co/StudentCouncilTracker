using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;

namespace StudentCouncilTracker.Application.Features.EventActions.Queries.GetById;

public record GetEventActionByIdQuery(int Id) : IRequest<EventActionDto>;

public class GetEventActionByIdQueryHandler(IEventActionRepository repository, IMapper mapper) : IRequestHandler<GetEventActionByIdQuery, EventActionDto>
{
    public async Task<EventActionDto> Handle(GetEventActionByIdQuery request, CancellationToken cancellationToken)
    {
        var source = await repository.GetByIdAsync(request.Id);
        var ret = mapper.Map<EventActionDto>(source);
        return ret;
    }
}