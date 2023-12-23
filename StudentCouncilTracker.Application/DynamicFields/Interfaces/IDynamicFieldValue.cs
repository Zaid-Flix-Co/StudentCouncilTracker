namespace StudentCouncilTracker.Application.DynamicFields.Interfaces;

/// <summary>
/// Interface IDynamicFieldValue
/// Extends the <see cref="IDynamicField" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IDynamicField" />
public interface IDynamicFieldValue<T> : IDynamicField
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    T Value { get; set; }
}