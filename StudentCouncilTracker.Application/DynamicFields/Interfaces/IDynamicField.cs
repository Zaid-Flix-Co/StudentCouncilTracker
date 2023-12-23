using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields.Interfaces;

/// <summary>
/// Interface IDynamicField
/// </summary>
public interface IDynamicField
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is editable.
    /// </summary>
    /// <value><c>true</c> if this instance is editable; otherwise, <c>false</c>.</value>
    bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is visible.
    /// </summary>
    /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
    bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is value hidden.
    /// </summary>
    /// <value><c>true</c> if this instance is value hidden; otherwise, <c>false</c>.</value>
    bool IsValueHidden { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>The type.</value>
    DynamicFieldType Type { get; set; }

    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    /// <value>The label.</value>
    DynamicFieldLabel Label { get; set; }

    /// <summary>
    /// Gets or sets the validators.
    /// </summary>
    /// <value>The validators.</value>
    List<string> Validators { get; set; }
}