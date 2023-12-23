namespace StudentCouncilTracker.Application.DynamicFields;

/// <summary>
/// Class DynamicFieldValue.
/// Implements the <see cref="DynamicField" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="DynamicField" />
public class DynamicFieldValue<T> : DynamicField
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicFieldValue{T}"/> class.
    /// </summary>
    public DynamicFieldValue()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicFieldValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public DynamicFieldValue(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    public virtual T Value { get; set; }
}