using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicProperties.Interfaces;

public interface IDynamicProperty
{
    bool IsEditable { get; set; }

    bool IsVisible { get; set; }

    bool IsValueHidden { get; set; }

    DynamicFieldType Type { get; set; }

    DynamicPropertyLabel Label { get; set; }

    List<string> Validators { get; set; }
}