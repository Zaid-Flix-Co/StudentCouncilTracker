using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Queries.GetByLogin;

public record GetUserByLoginQuery(CatalogUserDtoData Model) : IRequest<OperationResult<CatalogUserDto>>;

public class GetUserByLoginHandler(ICatalogUserRepository catalogUserRepository, IMapper mapper) : IRequestHandler<GetUserByLoginQuery, OperationResult<CatalogUserDto>>
{
    public async Task<OperationResult<CatalogUserDto>> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<CatalogUserDto>();
        var user = await catalogUserRepository.GetByLogin(request.Model);

        if (user == null)
        {
            operationResult.AddError("Неверно введен логин или пароль");
            return operationResult;
        }

        var ret = mapper.Map<CatalogUserDto>(user);
        operationResult.SetValue(ret);
        return operationResult;
    }
}