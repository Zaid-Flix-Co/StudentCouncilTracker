using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields;

public class DynamicField
{
    public bool IsEditable { get; set; }

    public bool IsVisible { get; set; }

    public bool IsValueHidden { get; set; }

    public string Name { get; set; }

    public DynamicFieldType Type { get; set; }

    public DynamicFieldLabel Label { get; set; } = new();

    public List<string> Validators { get; set; } = new();
}