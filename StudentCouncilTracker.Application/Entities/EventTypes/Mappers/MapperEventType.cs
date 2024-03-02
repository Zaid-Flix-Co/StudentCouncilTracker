using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Mappers;

public class MapperEventType : Profile
{
    public override string ProfileName => nameof(MapperEventType);

    public MapperEventType()
    {
        AllowNullDestinationValues = false;

        CreateMap<EventType, EventTypeDtoData>()
            .AfterMap<AfterMapEventTypeToDtoData>();

        CreateMap<EventType, EventTypeDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapEventTypeToDto>();

        this.CreateMapPropertyNullableId<EventType, EventTypeDtoCard>();
        this.CreateMapDynamicPropertyValue<EventType, EventTypeDtoCard>();

        CreateMap<EventType, EventTypeDtoCombobox>();
        this.CreateMapDynamicPropertyValue<EventType, EventTypeDtoCombobox>();
    }
}