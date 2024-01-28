using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Mappers;

public class MapperEventActionType : Profile
{
    public override string ProfileName => nameof(MapperEventActionType);

    public MapperEventActionType()
    {
        AllowNullDestinationValues = false;

        CreateMap<EventActionType, EventActionTypeDtoData>().AfterMap<AfterMapEventActionTypeToDtoData>();

        CreateMap<EventActionType, EventActionTypeDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapEventActionTypeToDto>();

        this.CreateMapPropertyNullableId<EventActionType, EventActionTypeDtoCard>();
        this.CreateMapDynamicPropertyValue<EventActionType, EventActionTypeDtoCard>();

        CreateMap<EventActionType, EventActionTypeDtoCombobox>();
        this.CreateMapDynamicPropertyValue<EventActionType, EventActionTypeDtoCombobox>();
    }
}