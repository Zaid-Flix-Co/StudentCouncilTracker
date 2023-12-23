using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields;

/// <summary>
/// Class DynamicFieldInfo.
/// </summary>
public class DynamicFieldInfo
{
    /// <summary>
    /// The field name
    /// </summary>
    private string? _fieldName;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is editable.
    /// </summary>
    /// <value><c>null</c> if [is editable] contains no value, <c>true</c> if [is editable]; otherwise, <c>false</c>.</value>
    public bool? IsEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is visible.
    /// </summary>
    /// <value><c>null</c> if [is visible] contains no value, <c>true</c> if [is visible]; otherwise, <c>false</c>.</value>
    public bool? IsVisible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is value hidden.
    /// </summary>
    /// <value><c>null</c> if [is value hidden] contains no value, <c>true</c> if [is value hidden]; otherwise, <c>false</c>.</value>
    public bool? IsValueHidden { get; set; }

    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    /// <value>The label.</value>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the label hint.
    /// </summary>
    /// <value>The label hint.</value>
    public string? LabelHint { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>The type.</value>
    public DynamicFieldType Type { get; set; }

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    /// <value>The name of the field.</value>
    public string FieldName
    {
        get => _fieldName ?? "";
        set => _fieldName = value;
    }

    /// <summary>
    /// Gets or sets the validators.
    /// </summary>
    /// <value>The validators.</value>
    public List<ValidationAttribute> Validators { get; set; } = new();

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString()
    {
        return $"{FieldName} {Label}";
    }
}