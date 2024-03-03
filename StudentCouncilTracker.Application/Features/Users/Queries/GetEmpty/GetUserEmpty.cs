using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Queries.GetEmpty;

public record GetUserEmptyQuery : IRequest<OperationResult<CatalogUserDto>>;

public class GetUserEmptyQueryHandler(ICatalogUserRepository catalogUserRepository, IMapper mapper) : IRequestHandler<GetUserEmptyQuery, OperationResult<CatalogUserDto>>
{
    public async Task<OperationResult<CatalogUserDto>> Handle(GetUserEmptyQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<CatalogUserDto>();
        var user = new CatalogUser();
        var ret = mapper.Map<CatalogUserDto>(user);
        operationResult.SetValue(ret);
        return operationResult;
    }
}