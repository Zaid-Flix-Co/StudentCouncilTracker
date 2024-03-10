using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActions.Queries.GetJournal;

public record GetEventActionJournalQuery(bool Flag, int Id, string UserName, Role Role) : IRequest<OperationResult<EventActionDtoJournal>>;

public class GetEventActionJournalQueryHandler(IEventActionRepository repository, IMapper mapper) : IRequestHandler<GetEventActionJournalQuery, OperationResult<EventActionDtoJournal>>
{
    public async Task<OperationResult<EventActionDtoJournal>> Handle(GetEventActionJournalQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventActionDtoJournal>();
        var eventActions = repository
            .GetAll();

        var isChairman = request.Role == Role.Chairman;
        bool isResponsibleUser;

        if (request.Flag)
        {
            eventActions = eventActions
                .Where(e => e!.EventId == request.Id)
                #pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                .Include(e => e!.EventActionType)
                .Include(e => e!.Status)
                .Include(e => e!.ResponsibleManager)
                .Include(e => e!.Event)
                    .ThenInclude(e => e.ResponsibleUser)
                #pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                .OrderByDescending(e => e!.CreatedDate)
                .AsNoTracking();

            isResponsibleUser = (await eventActions.FirstOrDefaultAsync(cancellationToken: cancellationToken))?.Event.ResponsibleUser?.Name ==
                                request.UserName;
        }
        else
        {
            eventActions = eventActions
                .Where(e => e!.ResponsibleManagerId == request.Id)
                #pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                .Include(e => e!.EventActionType)
                .Include(e => e!.Status)
                .Include(e => e!.ResponsibleManager)
                .Include(e => e!.Event)
                    .ThenInclude(e => e.ResponsibleUser)
                #pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                .OrderByDescending(e => e!.CreatedDate)
                .AsNoTracking();

            isResponsibleUser = false;
        }

        var journalDto = new EventActionDtoJournal
        {
            Items = await eventActions.Select(s => mapper.Map<EventActionDtoJournalItem>(s)).ToListAsync(cancellationToken: cancellationToken),
            Permissions = new JournalPermission
            {
                Create = isChairman || isResponsibleUser
            },
            QueryString = string.Empty,
            TotalCount = await eventActions.CountAsync(cancellationToken: cancellationToken)
        };

        operationResult.SetValue(journalDto);

        return operationResult;
    }
}