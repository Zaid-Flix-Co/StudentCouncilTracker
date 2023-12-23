using AutoMapper;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;

namespace StudentCouncilTracker.Application.Entities.Users.Mappers;

public class AfterMapCatalogUserToDtoData : IMappingAction<CatalogUser, CatalogUserDtoData>
{
    public void Process(CatalogUser source, CatalogUserDtoData destination, ResolutionContext context)
    {

    }
}