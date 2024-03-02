using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Mappers;

public class MapperEventActionStatus : Profile
{
    public override string ProfileName => nameof(MapperEventActionStatus);

    public MapperEventActionStatus()
    {
        AllowNullDestinationValues = false;

        CreateMap<EventActionStatus, EventActionStatusDtoData>().AfterMap<AfterMapEventActionStatusToDtoData>();

        CreateMap<EventActionStatus, EventActionStatusDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapEventActionStatusToDto>();

        this.CreateMapPropertyNullableId<EventActionStatus, EventActionStatusDtoCard>();
        this.CreateMapDynamicPropertyValue<EventActionStatus, EventActionStatusDtoCard>();

        CreateMap<EventActionStatus, EventActionStatusDtoCombobox>();
        this.CreateMapDynamicPropertyValue<EventActionStatus, EventActionStatusDtoCombobox>();
    }
}