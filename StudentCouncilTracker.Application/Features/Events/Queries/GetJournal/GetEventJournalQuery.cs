using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Events.Queries.GetJournal;

public record GetEventJournalQuery : IRequest<OperationResult<EventDtoJournal>>;

public class GetEventJournalQueryHandler(IEventRepository repository, IMapper mapper) : IRequestHandler<GetEventJournalQuery, OperationResult<EventDtoJournal>>
{
    public async Task<OperationResult<EventDtoJournal>> Handle(GetEventJournalQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<EventDtoJournal>();
        var events = repository
            .GetAll()
            .Include(e => e.EventType)
            .Include(e => e.ResponsibleUser)
            .OrderByDescending(e => e.CreatedDate)
            .AsNoTracking();

        var journalDto = new EventDtoJournal
        {
            Items = await events.Select(s => mapper.Map<EventDtoJournalItem>(s)).ToListAsync(cancellationToken: cancellationToken),
            Permissions = new JournalPermission
            {
                Create = true,
                CanPrint = true,
                CanChangePrintSetting = true
            },
            QueryString = string.Empty,
            TotalCount = await events.CountAsync(cancellationToken: cancellationToken)
        };

        operationResult.SetValue(journalDto);

        return operationResult;
    }
}