using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<OperationResult<CatalogUserDto>>;

public class CreateCatalogUserCommandHandler(ICatalogUserRepository repository, IMapper mapper) : IRequestHandler<CreateUserCommand, OperationResult<CatalogUserDto>>
{
    public async Task<OperationResult<CatalogUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new CatalogUserDto());
        var catalogUser = new CatalogUser();
        repository.Insert(catalogUser);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var user = await repository.GetCardByIdAsync(catalogUser.Id);
        var dto = mapper.Map<CatalogUserDto>(user);
        result.SetValue(dto);

        return result;
    }
}
