using System.ComponentModel.DataAnnotations;
using System.Reflection;
using StudentCouncilTracker.Application.DynamicFields.Attributes;
using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields.Helpers;

public static class DynamicFieldHelper
{
    public const bool DefaultVisible = true;

    public const bool DefaultEditable = false;

    public const bool DefaultValueHidden = false;

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

        return new DynamicFieldInfo
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