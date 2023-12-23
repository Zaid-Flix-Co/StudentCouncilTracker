using AutoMapper;
using StudentCouncilTracker.Application.DynamicFields;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Extensions;

public static class MapperExtensions
{
    public static IMappingExpression<TSource, TDestination> CreateMapEntityDto<TSource, TDestination, TDestinationData>(this Profile profile)
        where TDestination : EntityDto<TDestinationData>
        where TDestinationData : IHaveId, new()
    {
        return profile.CreateMap<TSource, TDestination>()
            .ForMember(f => f.Data, o => o.MapFrom(f => f))
            .ForMember(f => f.Permissions, o => o.Ignore());
    }

    public static IMappingExpression<TSource, TDestination> CreateMapPropertyNullableId<TSource, TDestination>(this Profile profile)
        where TSource : IHaveId
        where TDestination : IHaveId<int?>
    {
        return profile.CreateMap<TSource, TDestination>()
            .ForMember(f => f.Id, z => z.MapFrom(f => f.Id == 0 ? null : (int?)f.Id));
    }

    public static IMappingExpression<TSource, DynamicFieldValue<TDestination>> CreateMapDynamicPropertyValue<TSource, TDestination>(this Profile profile)
    {
        return profile.CreateMap<TSource, DynamicFieldValue<TDestination>>()
            .ForMember(f => f.IsEditable, o => o.Ignore())
            .ForMember(f => f.IsVisible, o => o.Ignore())
            .ForMember(f => f.IsValueHidden, o => o.Ignore())
            .ForMember(f => f.Label, o => o.Ignore())
            .ForMember(f => f.Type, o => o.Ignore())
            .ForMember(f => f.Name, o => o.Ignore())
            .ForMember(f => f.Validators, o => o.Ignore())
            .ForMember(f => f.Value, o => o.MapFrom(f => f));
    }
}