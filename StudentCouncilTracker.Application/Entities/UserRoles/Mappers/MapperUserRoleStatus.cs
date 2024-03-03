using AutoMapper;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.UserRoles.Mappers;

public class MapperUserRole : Profile
{
    public override string ProfileName => nameof(MapperUserRole);

    public MapperUserRole()
    {
        AllowNullDestinationValues = false;

        CreateMap<UserRole, UserRoleDtoData>().AfterMap<AfterMapUserRoleToDtoData>();

        CreateMap<UserRole, UserRoleDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapUserRoleToDto>();

        this.CreateMapPropertyNullableId<UserRole, UserRoleDtoCard>();
        this.CreateMapDynamicPropertyValue<UserRole, UserRoleDtoCard>();

        CreateMap<UserRole, UserRoleDtoCombobox>();
        this.CreateMapDynamicPropertyValue<UserRole, UserRoleDtoCombobox>();
    }
}