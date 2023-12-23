using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields;

/// <summary>
/// Class DynamicField.
/// </summary>
public class DynamicField
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is editable.
    /// </summary>
    /// <value><c>true</c> if this instance is editable; otherwise, <c>false</c>.</value>
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is visible.
    /// </summary>
    /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
    public bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is value hidden.
    /// </summary>
    /// <value><c>true</c> if this instance is value hidden; otherwise, <c>false</c>.</value>
    public bool IsValueHidden { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>The type.</value>
    public DynamicFieldType Type { get; set; }

    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    /// <value>The label.</value>
    public DynamicFieldLabel Label { get; set; } = new();

    /// <summary>
    /// Gets or sets the validators.
    /// </summary>
    /// <value>The validators.</value>
    public List<string> Validators { get; set; } = new();
}