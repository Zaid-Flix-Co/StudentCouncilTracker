using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Queries.GetJournal;

public record GetCatalogUserJournalQuery : IRequest<OperationResult<CatalogUserDtoJournal>>;

public class GetCatalogUserJournalQueryHandler(ICatalogUserRepository repository, IMapper mapper) : IRequestHandler<GetCatalogUserJournalQuery, OperationResult<CatalogUserDtoJournal>>
{
    public async Task<OperationResult<CatalogUserDtoJournal>> Handle(GetCatalogUserJournalQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<CatalogUserDtoJournal>();
        var users = repository
            .GetAll()
            .Include(u => u.Role)
            .AsNoTracking();

        var journalDto = new CatalogUserDtoJournal
        {
            Items = await users.Select(s => mapper.Map<CatalogUserDtoJournalItem>(s)).ToListAsync(cancellationToken: cancellationToken),
            Permissions = new JournalPermission
            {
                Create = true,
                CanPrint = true,
                CanChangePrintSetting = true
            },
            QueryString = string.Empty,
            TotalCount = await users.CountAsync(cancellationToken: cancellationToken)
        };

        operationResult.SetValue(journalDto);

        return operationResult;
    }
}