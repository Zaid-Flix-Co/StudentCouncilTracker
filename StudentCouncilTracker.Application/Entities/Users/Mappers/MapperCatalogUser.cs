using AutoMapper;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.Users.Mappers;

public class MapperCatalogUser : Profile
{
    public override string ProfileName => nameof(MapperCatalogUser);

    public MapperCatalogUser()
    {
        AllowNullDestinationValues = false;

        CreateMap<CatalogUser, CatalogUserDtoData>().AfterMap<AfterMapCatalogUserToDtoData>();

        CreateMap<CatalogUser, CatalogUserDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapCatalogUserToDto>();

        CreateMap<CatalogUser, CatalogUserDtoJournalItem>()
            .ForMember(d => d.Role, s => s.MapFrom(f => f.Role!.Name))
            .ForMember(d => d.Permissions, s => s.Ignore())
            .AfterMap<AfterMapCatalogUserToDtoJournalItem>();

        this.CreateMapPropertyNullableId<CatalogUser, CatalogUserDtoCard>();
        this.CreateMapDynamicPropertyValue<CatalogUser, CatalogUserDtoCard>();

        CreateMap<CatalogUser, CatalogUserDtoCombobox>();
        this.CreateMapDynamicPropertyValue<CatalogUser, CatalogUserDtoCombobox>();
    }
}