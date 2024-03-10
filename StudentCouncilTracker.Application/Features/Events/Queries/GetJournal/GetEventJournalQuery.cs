using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Events.Queries.GetJournal;

public record GetEventJournalQuery(string UserName, Role Role) : IRequest<OperationResult<EventDtoJournal>>;

public class GetEventJournalQueryHandler(IEventRepository repository, IMapper mapper) : IRequestHandler<GetEventJournalQuery, OperationResult<EventDtoJournal>>
{
    public async Task<OperationResult<EventDtoJournal>> Handle(GetEventJournalQuery request, CancellationToken cancellationToken)
    {
        var isChairman = request.Role == Role.Chairman;
        var operationResult = new OperationResult<EventDtoJournal>();
        var events = repository
            .GetAll()
            #pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            .Include(e => e!.EventType)
            .Include(e => e!.ResponsibleUser)
            #pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            .OrderByDescending(e => e!.CreatedDate)
            .AsNoTracking();

        var journalDto = new EventDtoJournal
        {
            Items = await events.Select(s => mapper.Map<EventDtoJournalItem>(s)).ToListAsync(cancellationToken: cancellationToken),
            Permissions = new JournalPermission
            {
                Create = isChairman
            },
            QueryString = string.Empty,
            TotalCount = await events.CountAsync(cancellationToken: cancellationToken)
        };

        operationResult.SetValue(journalDto);

        return operationResult;
    }
}