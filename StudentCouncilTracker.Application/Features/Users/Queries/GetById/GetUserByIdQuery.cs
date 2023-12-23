using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;

namespace StudentCouncilTracker.Application.Features.Users.Queries.GetById;

public record GetUserByIdQuery(int Id) : IRequest<CatalogUserDto>;

public class GetUserByIdQueryHandler(ICatalogUserRepository catalogUserRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, CatalogUserDto>
{
    public async Task<CatalogUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await catalogUserRepository.GetByIdAsync(request.Id);
        var ret = mapper.Map<CatalogUserDto>(user);
        return ret;
    }
}