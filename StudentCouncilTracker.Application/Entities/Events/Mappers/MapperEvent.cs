using AutoMapper;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.Events.Mappers;

public class MapperEvent : Profile
{
    public override string ProfileName => nameof(MapperEvent);

    public MapperEvent()
    {
        AllowNullDestinationValues = false;

        CreateMap<Event, EventDtoData>().AfterMap<AfterMapEventToDtoData>();

        CreateMap<Event, EventDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapEventToDto>();

        this.CreateMapPropertyNullableId<Event, EventDtoCard>();
        this.CreateMapDynamicPropertyValue<Event, EventDtoCard>();

        CreateMap<Event, EventDtoCombobox>();
        this.CreateMapDynamicPropertyValue<Event, EventDtoCombobox>();
    }
}