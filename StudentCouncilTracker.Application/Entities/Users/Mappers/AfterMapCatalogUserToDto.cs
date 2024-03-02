using AutoMapper;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;

namespace StudentCouncilTracker.Application.Entities.Users.Mappers;

public class AfterMapCatalogUserToDto : IMappingAction<CatalogUser, CatalogUserDto>
{
    public void Process(CatalogUser source, CatalogUserDto destination, ResolutionContext context)
    {

    }
}