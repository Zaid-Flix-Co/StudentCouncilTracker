using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicProperties;

public class DynamicProperty
{
    public bool IsEditable { get; set; }

    public bool IsVisible { get; set; }

    public bool IsValueHidden { get; set; }

    public DynamicFieldType Type { get; set; }

    public DynamicPropertyLabel Label { get; set; } = new();

    public List<string> Validators { get; set; } = new();
}