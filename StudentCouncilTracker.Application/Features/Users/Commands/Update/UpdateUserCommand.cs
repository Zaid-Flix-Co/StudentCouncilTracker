using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Commands.Update;

public record UpdateUserCommand(int Id, CatalogUserDtoData Model, string UserName) : IRequest<OperationResult<CatalogUserDto>>;

public class UpdateCatalogUserCommandHandler(ICatalogUserRepository repository, IMapper mapper) : IRequestHandler<UpdateUserCommand, OperationResult<CatalogUserDto>>
{
    public async Task<OperationResult<CatalogUserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new CatalogUserDto());
        var id = request.Id;
        var stored = await repository.GetByIdAsync(id);

        if (stored == null)
        {
            result.AddError($"Entity with identifier {id} not found");
            return result;
        }

        var res = stored.Edit(request.Model, request.UserName);
        if(!res.Ok)
        {
            result.AddReasons(res.Reasons);
            return result;
        }

        repository.Update(stored);

        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var card = await repository.GetCardByIdAsync(id);
        var dto = mapper.Map<CatalogUserDto>(card);
        result.SetValue(dto);

        return result;
    }
}