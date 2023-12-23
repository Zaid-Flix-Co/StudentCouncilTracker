using System.ComponentModel.DataAnnotations;
using System.Reflection;
using StudentCouncilTracker.Application.DynamicFields.Attributes;
using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields.Helpers;

/// <summary>
/// Class DynamicFieldHelper.
/// </summary>
public static class DynamicFieldHelper
{
    /// <summary>
    /// The default visible
    /// </summary>
    public const bool DefaultVisible = true;

    /// <summary>
    /// The default editable
    /// </summary>
    public const bool DefaultEditable = false;

    /// <summary>
    /// The default value hidden
    /// </summary>
    public const bool DefaultValueHidden = false;

    /// <summary>
    /// Creates the fields.
    /// </summary>
    /// <param name="sourceValue">The source value.</param>
    /// <param name="destinationValue">The destination value.</param>
    /// <param name="dynProps">The dyn props.</param>
    /// <param name="requireds">The requireds.</param>
    /// <returns>List&lt;BaseDynamicField&gt;.</returns>
    public static List<DynamicFieldInfo> CreateFields(object sourceValue, object destinationValue, List<DynamicFieldInfo> dynProps, List<string> requireds)
    {
        var ret = new List<DynamicFieldInfo>();
        var sourceType = sourceValue.GetType();
        var destinationType = destinationValue.GetType();
        var destinationProps =TypeHolder.GetProperties(destinationType);//properties are cached in memory
        var sourceProps = TypeHolder.GetProperties(sourceType).ToList();
        foreach (var p in destinationProps)
        {
            var sourceProperty = sourceProps.Find(f => f.Name.Equals(p.Name));//Source property. Его может и не быть в Domain классе
            var property = CreateField(sourceProperty, p, sourceType, destinationType, dynProps, requireds);
            ret.Add(property);
        }
        return ret;
    }

    /// <summary>
    /// Creates the field.
    /// </summary>
    /// <param name="sourceProperty">The source property.</param>
    /// <param name="destinationProperty">The destination property.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <param name="dynProps">The dyn props.</param>
    /// <param name="requireds">The requireds.</param>
    /// <returns>DynamicFieldInfo.</returns>
    private static DynamicFieldInfo CreateField(PropertyInfo? sourceProperty, PropertyInfo destinationProperty, Type? sourceType, Type? destinationType, List<DynamicFieldInfo> dynProps, List<string> requireds)
    {
        var type = DynamicFieldType.Textbox;
        var pName = destinationProperty.Name;
        var label = pName;
        var isVisible = DefaultVisible;
        var isEditable = DefaultEditable;
        var isValueHidden = DefaultValueHidden;
        var validators = new List<ValidationAttribute>();
        string labelHint = null;

        if (sourceProperty != null)
        {
            validators.AddRange(sourceProperty.GetCustomAttributes(typeof(ValidationAttribute)).Cast<ValidationAttribute>());

            if (requireds.Find(s => s == sourceProperty.Name) != null)
                validators.Add(new RequiredAttribute());

            label = (TypeHolder.GetAttributes(sourceType!, sourceProperty, typeof(DisplayAttribute)) as DisplayAttribute)?.Name ?? label;
        }

        var destDynAttr = TypeHolder.GetAttributes(destinationType!, destinationProperty, typeof(DynamicFieldAttribute));
        if (destDynAttr is DynamicFieldAttribute attr)
        {
            type = attr.FieldType;
            label = attr.Label ?? label;
            labelHint = attr.LabelHint;
            isEditable = attr.IsEditable;
            isVisible = attr.IsVisible;
        }

        var dynProp = dynProps.Find(f => f.FieldName.Equals(pName, StringComparison.OrdinalIgnoreCase));
        if (dynProp != null)
        {
            label = dynProp.Label ?? label;
            labelHint = dynProp.LabelHint ?? labelHint;
            isVisible = dynProp.IsVisible ?? isVisible;
            isEditable = dynProp.IsEditable ?? isEditable;
            isValueHidden = dynProp.IsValueHidden ?? isValueHidden;
            validators = dynProp.Validators ?? validators;
        }

        return new DynamicFieldInfo()
        {
            FieldName = pName,
            Label = label,
            LabelHint = labelHint ?? "",
            IsVisible = isVisible,
            IsEditable = isEditable,
            IsValueHidden = isValueHidden,
            Type = type,
            Validators = (validators.Any() ? validators : null)!
        };
    }

    /// <summary>
    /// Creates the default field.
    /// </summary>
    /// <param name="pName">Name of the p.</param>
    /// <returns>DynamicFieldInfo.</returns>
    public static DynamicFieldInfo CreateDefaultField(string pName)
    {
        return new DynamicFieldInfo
        {
            FieldName = pName,
            Label = pName,
            LabelHint = null,
            IsVisible = DefaultVisible,
            IsEditable = DefaultEditable,
            IsValueHidden = DefaultValueHidden,
            Type = DynamicFieldType.Textbox
        };
    }
}