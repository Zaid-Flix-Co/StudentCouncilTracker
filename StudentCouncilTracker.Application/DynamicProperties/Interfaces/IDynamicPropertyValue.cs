namespace StudentCouncilTracker.Application.DynamicProperties.Interfaces;

public interface IDynamicPropertyValue<T> : IDynamicProperty
{
    T Value { get; set; }
}