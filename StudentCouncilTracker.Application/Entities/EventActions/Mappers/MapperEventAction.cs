using AutoMapper;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Dto.Combobox;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.Entities.EventActions.Mappers;

public class MapperEventAction : Profile
{
    public override string ProfileName => nameof(MapperEventAction);

    public MapperEventAction()
    {
        AllowNullDestinationValues = false;

        CreateMap<EventAction, EventActionDtoData>().AfterMap<AfterMapEventActionToDtoData>();

        CreateMap<EventAction, EventActionDto>()
            .ForMember(c => c.Data, opt => opt.MapFrom(f => f))
            .ForMember(c => c.Permissions, opt => opt.Ignore())
            .AfterMap<AfterMapEventActionToDto>();
        
        CreateMap<EventAction, EventActionDtoJournalItem>()
            .ForMember(d => d.ResponsibleManager, s => s.MapFrom(f => f.ResponsibleManager!.Name))
            .ForMember(d => d.EventActionType, s => s.MapFrom(f => f.EventActionType!.Name))
            .ForMember(d => d.Status, s => s.MapFrom(f => f.Status!.Name))
            .ForMember(d => d.DeadlineCompletion, s => s.MapFrom(f => f.DeadlineCompletion))
            .ForMember(d => d.Permissions, s => s.Ignore())
            .AfterMap<AfterMapEventActionToDtoJournalItem>();

        this.CreateMapPropertyNullableId<EventAction, EventActionDtoCard>();
        this.CreateMapDynamicPropertyValue<EventAction, EventActionDtoCard>();

        CreateMap<EventAction, EventActionDtoCombobox>();
        this.CreateMapDynamicPropertyValue<EventAction, EventActionDtoCombobox>();
    }
}