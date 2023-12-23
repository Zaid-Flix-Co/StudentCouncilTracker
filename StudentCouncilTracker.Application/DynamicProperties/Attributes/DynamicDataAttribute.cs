using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.DataAnnotations;

namespace StudentCouncilTracker.Application.DynamicProperties.Attributes;

public class DynamicDataAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DataAnnotationsValidator.TryValidateRecursive(value, validationContext);

        // Validation of the properties on the complex type are responsible for adding their own messages.
        // Therefore, we can always return success from here.
        return ValidationResult.Success;
    }
}