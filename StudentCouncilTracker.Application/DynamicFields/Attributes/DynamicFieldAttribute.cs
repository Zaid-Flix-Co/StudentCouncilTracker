using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.DataAnnotations;
using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DynamicFieldAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        DataAnnotationsValidator.TryValidateRecursive(value!, validationContext);
        return ValidationResult.Success!;
    }

    public DynamicFieldAttribute(DynamicFieldType fieldType, string label = null!, string labelHint = null!)
    {
        FieldType = fieldType;
        Label = label;
        LabelHint = labelHint;
    }

    public DynamicFieldAttribute(DynamicFieldType fieldType, string label)
    {
        FieldType = fieldType;
        Label = label;
    }

    public DynamicFieldType FieldType { get; }

    public string Label { get; set; }

    public string LabelHint { get; set; } = null!;

    public bool IsEditable { get; set; } = true;

    public bool IsVisible { get; set; } = true;
}