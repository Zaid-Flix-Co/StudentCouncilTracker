namespace StudentCouncilTracker.Application.DynamicFields;

public sealed class DynamicFieldValue<T> : DynamicField
{
    public DynamicFieldValue()
    {

    }

    public DynamicFieldValue(T value)
    {
        Value = value;
    }

    public T Value { get; set; }
}