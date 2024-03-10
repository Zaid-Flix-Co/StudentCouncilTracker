using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.DataAnnotations;

namespace StudentCouncilTracker.Application.DynamicProperties.Attributes;

public class DynamicDataAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        DataAnnotationsValidator.TryValidateRecursive(value!, validationContext);
        return ValidationResult.Success!;
    }
}