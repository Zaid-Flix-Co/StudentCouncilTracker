using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields;

public class DynamicFieldInfo
{
    private string? _fieldName;

    public bool? IsEditable { get; set; }

    public bool? IsVisible { get; set; }

    public bool? IsValueHidden { get; set; }

    public string? Label { get; set; }

    public string? LabelHint { get; set; }

    public DynamicFieldType Type { get; set; }

    public string FieldName
    {
        get => _fieldName ?? "";
        set => _fieldName = value;
    }

    public List<ValidationAttribute> Validators { get; set; } = new();

    public override string ToString()
    {
        return $"{FieldName} {Label}";
    }
}