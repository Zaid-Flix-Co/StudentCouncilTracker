using AutoMapper;

namespace StudentCouncilTracker.Application.DynamicFields.Mappers;

/// <summary>
/// Class AfterMapEntityPropertyToDynamicFieldValue.
/// Implements the <see cref="AutoMapper.IMappingAction{System.Object, System.Object}" />
/// </summary>
/// <seealso cref="AutoMapper.IMappingAction{System.Object, System.Object}" />
public class AfterMapEntityPropertyToDynamicFieldValue : IMappingAction<object, object>
{
    /// <summary>
    /// Implementors can modify both the source and destination objects
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="destination">Destination object</param>
    /// <param name="context">Resolution context</param>
    public void Process(object source, object destination, ResolutionContext context)
    {

    }
}