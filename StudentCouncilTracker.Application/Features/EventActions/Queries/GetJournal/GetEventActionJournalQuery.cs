using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.EventActions.Queries.GetJournal;

public record GetEventActionJournalQuery(int EventId) : IRequest<OperationResult<EventActionDtoJournal>>;

public class GetEventActionJournalQueryHandler(IEventActionRepository repository, IMapper mapper) : IRequestHandler<GetEventActionJournalQuery, OperationResult<EventActionDtoJournal>>
{
    public async Task<OperationResult<EventActionDtoJournal>> Handle(GetEventActionJournalQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventActionDtoJournal>();
        var eventActions = repository
            .GetAll()
            .Where(e => e.EventId == request.EventId)
            .Include(e => e.EventActionType)
            .Include(e => e.Status)
            .Include(e => e.ResponsibleManager)
            .OrderByDescending(e => e.CreatedDate)
            .AsNoTracking();

        var journalDto = new EventActionDtoJournal
        {
            Items = await eventActions.Select(s => mapper.Map<EventActionDtoJournalItem>(s)).ToListAsync(cancellationToken: cancellationToken),
            Permissions = new JournalPermission
            {
                Create = true,
                CanPrint = true,
                CanChangePrintSetting = true
            },
            QueryString = string.Empty,
            TotalCount = await eventActions.CountAsync(cancellationToken: cancellationToken)
        };

        operationResult.SetValue(journalDto);

        return operationResult;
    }
}