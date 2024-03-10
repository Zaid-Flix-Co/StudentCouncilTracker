using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Queries.GetJournal;

public record GetCatalogUserJournalQuery(string UserName, Role Role, bool IsActive) : IRequest<OperationResult<CatalogUserDtoJournal>>;

public class GetCatalogUserJournalQueryHandler(ICatalogUserRepository repository, IMapper mapper) : IRequestHandler<GetCatalogUserJournalQuery, OperationResult<CatalogUserDtoJournal>>
{
    public async Task<OperationResult<CatalogUserDtoJournal>> Handle(GetCatalogUserJournalQuery request, CancellationToken cancellationToken)
    {
        var isChairman = request.Role == Role.Chairman;
        var operationResult = new OperationResult<CatalogUserDtoJournal>();
        var users = repository
            .GetAll();

        if (!request.IsActive)
        {
            users = users
                #pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                .Include(u => u!.Role)
                .AsNoTracking();
                #pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        }
        else
        {
            users = users
                .Where(u => u!.IsDeactivated)
                #pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                .Include(u => u!.Role)
                .AsNoTracking();
                #pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        }

        var journalDto = new CatalogUserDtoJournal
        {
            Items = await users.Select(s => mapper.Map<CatalogUserDtoJournalItem>(s)).ToListAsync(cancellationToken: cancellationToken),
            Permissions = new JournalPermission
            {
                Create = isChairman
            },
            QueryString = string.Empty,
            TotalCount = await users.CountAsync(cancellationToken: cancellationToken)
        };

        operationResult.SetValue(journalDto);

        return operationResult;
    }
}