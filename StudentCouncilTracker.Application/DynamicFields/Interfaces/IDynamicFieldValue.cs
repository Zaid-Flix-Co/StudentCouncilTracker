namespace StudentCouncilTracker.Application.DynamicFields.Interfaces;

public interface IDynamicFieldValue<T> : IDynamicField
{
    T Value { get; set; }
}