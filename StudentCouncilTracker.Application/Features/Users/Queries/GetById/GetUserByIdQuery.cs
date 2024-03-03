using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Users.Queries.GetById;

public record GetUserByIdQuery(int Id) : IRequest<OperationResult<CatalogUserDto>>;

public class GetUserByIdQueryHandler(ICatalogUserRepository catalogUserRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, OperationResult<CatalogUserDto>>
{
    public async Task<OperationResult<CatalogUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<CatalogUserDto>();
        var user = await catalogUserRepository.GetByIdAsync(request.Id);
        var ret = mapper.Map<CatalogUserDto>(user); 
        operationResult.SetValue(ret);
        return operationResult;
    }
}