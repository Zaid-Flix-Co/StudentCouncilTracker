using StudentCouncilTracker.Application.DataAnnotations;
using StudentCouncilTracker.Application.DynamicFields.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentCouncilTracker.Application.DynamicProperties.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DynamicPropertyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DataAnnotationsValidator.TryValidateRecursive(value, validationContext);
        return ValidationResult.Success;
    }

    public DynamicPropertyAttribute(DynamicFieldType fieldType, string label = null, string labelHint = null)
    {
        FieldType = fieldType;
        Label = label;
        LabelHint = labelHint;
    }

    public DynamicPropertyAttribute(DynamicFieldType fieldType, string label)
    {
        FieldType = fieldType;
        Label = label;
    }

    public DynamicFieldType FieldType { get; }

    public string Label { get; set; } = null;

    public string LabelHint { get; set; } = null;

    public bool IsEditable { get; set; } = true;

    public bool IsVisible { get; set; } = true;
}