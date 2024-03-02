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

        CreateMap<Event, EventDtoJournalItem>()
            .ForMember(d => d.EventType, s => s.MapFrom(f => f.EventType!.Name))
            .ForMember(d => d.ResponsibleUser, s => s.MapFrom(f => f.ResponsibleUser!.Name))
            .ForMember(d => d.Permissions, s => s.Ignore())
            .AfterMap<AfterMapEventToDtoJournalItem>();

        this.CreateMapPropertyNullableId<Event, EventDtoCard>();
        this.CreateMapDynamicPropertyValue<Event, EventDtoCard>();

        CreateMap<Event, EventDtoCombobox>();
        this.CreateMapDynamicPropertyValue<Event, EventDtoCombobox>();
    }
}