using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields.Interfaces;

public interface IDynamicField
{
    bool IsEditable { get; set; }

    bool IsVisible { get; set; }

    bool IsValueHidden { get; set; }

    DynamicFieldType Type { get; set; }

    DynamicFieldLabel Label { get; set; }

    List<string> Validators { get; set; }
}