using AutoMapper;
using StudentCouncilTracker.Application.Entities.Base.Dto;

namespace StudentCouncilTracker.Application.DynamicFields.Mappers;

public class MapperDynamicField : Profile
{
    public override string ProfileName => nameof(MapperDynamicField);

    public MapperDynamicField()
    {
        AllowNullDestinationValues = false;

        CreateMap<object, DynamicField>()
            .ForMember(f => f.IsEditable, o => o.Ignore())
            .ForMember(f => f.IsVisible, o => o.Ignore())
            .ForMember(f => f.IsValueHidden, o => o.Ignore())
            .ForMember(f => f.Label, o => o.Ignore())
            .ForMember(f => f.Type, o => o.Ignore())
            .ForMember(f => f.Validators, o => o.Ignore())
            .ForMember(f => f.Name, o => o.Ignore())
            .IncludeAllDerived()
            ;

        CreateMap(typeof(object), typeof(DynamicFieldValue<>))
            .ForMember("Value", o => o.MapFrom(m => m))
            .IncludeAllDerived()
            .AfterMap<AfterMapEntityPropertyToDynamicFieldValue>()
            ;

        CreateMap(typeof(object), typeof(EntityDto<>))
            .ForMember("Data", options => options.MapFrom(m => m))
            .ForMember("Permissions", options => options.Ignore())
            .IncludeAllDerived()
            .AfterMap<AfterMapEntityToEntityDto>()
            ;

        CreateMap(typeof(object), typeof(EntityDtoData))
            .IncludeAllDerived()
            .AfterMap<AfterMapEntityToEntityDtoData>()
            ;
    }
}