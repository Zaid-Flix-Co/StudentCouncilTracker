namespace StudentCouncilTracker.Application.DynamicProperties;

public class DynamicPropertyValue<T> : DynamicProperty
{
    public DynamicPropertyValue()
    {
    }

    public DynamicPropertyValue(T value)
    {
        Value = value;
    }

    public virtual T Value { get; set; }
}