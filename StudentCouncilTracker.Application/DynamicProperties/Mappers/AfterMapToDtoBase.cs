using AutoMapper;

namespace StudentCouncilTracker.Application.DynamicProperties.Mappers;

public abstract class AfterMapToDtoBase<TSource, TDestination> : IMappingAction<TSource, TDestination>
{
    public void Process(TSource source, TDestination destination, ResolutionContext context)
    {
        throw new NotImplementedException();
    }
}