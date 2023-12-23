using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace StudentCouncilTracker.Application.DataAnnotations;

public class DataAnnotationsValidator : ComponentBase
{
    private static readonly object ValidationContextValidatorKey = new();

    private static readonly object ValidatedObjectsKey = new();

    private ValidationMessageStore _validationMessageStore;

    [CascadingParameter]
    internal EditContext EditContext { get; set; }

    protected override void OnInitialized()
    {
        _validationMessageStore = new ValidationMessageStore(EditContext);

        EditContext.OnValidationRequested += (sender, eventArgs) =>
        {
            _validationMessageStore.Clear();
            ValidateObject(EditContext.Model, new HashSet<object>());
            EditContext.NotifyValidationStateChanged();
        };

        EditContext.OnFieldChanged += (sender, eventArgs) =>
            ValidateField(EditContext, _validationMessageStore, eventArgs.FieldIdentifier);
    }

    internal void ValidateObject(object value, HashSet<object> visited)
    {
        if (value is null)
            return;

        if (!visited.Add(value))
            return;

        if (value is IEnumerable<object> enumerable)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                ValidateObject(item, visited);
                index++;
            }

            return;
        }

        var validationResults = new List<ValidationResult>();
        ValidateObject(value, visited, validationResults);

        foreach (var validationResult in validationResults)
        {
            if (!validationResult.MemberNames.Any())
            {
                _validationMessageStore.Add(new FieldIdentifier(value, string.Empty), validationResult.ErrorMessage);
                continue;
            }

            foreach (var memberName in validationResult.MemberNames)
            {
                var fieldIdentifier = new FieldIdentifier(value, memberName);
                _validationMessageStore.Add(fieldIdentifier, validationResult.ErrorMessage);
            }
        }
    }

    private void ValidateObject(object value, HashSet<object> visited, List<ValidationResult> validationResults)
    {
        var validationContext = new ValidationContext(value);
        validationContext.Items.Add(ValidationContextValidatorKey, this);
        validationContext.Items.Add(ValidatedObjectsKey, visited);
        Validator.TryValidateObject(value, validationContext, validationResults, validateAllProperties: true);
    }

    internal static bool TryValidateRecursive(object value, ValidationContext validationContext)
    {
        if (validationContext.Items.TryGetValue(ValidationContextValidatorKey, out var result) && result is DataAnnotationsValidator validator)
        {
            var visited = (HashSet<object>)validationContext.Items[ValidatedObjectsKey];
            validator.ValidateObject(value, visited);

            return true;
        }

        return false;
    }

    private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier)
    {
        var propertyInfo = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName);
        if (propertyInfo != null)
        {
            var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
            var validationContext = new ValidationContext(fieldIdentifier.Model)
            {
                MemberName = propertyInfo.Name
            };
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, validationContext, results);
            messages.Clear(fieldIdentifier);
            messages.Add(fieldIdentifier, results.Select(result => result.ErrorMessage));

            editContext.NotifyValidationStateChanged();
        }
    }
}