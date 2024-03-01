using StudentCouncilTracker.Application.DynamicFields.Enums;

namespace StudentCouncilTracker.Application.DynamicFields;

public class DynamicField
{
    public bool IsEditable { get; set; } = true;

    public bool IsVisible { get; set; } = true;

    public bool IsValueHidden { get; set; } = false;

    public string Name { get; set; }

    public DynamicFieldType Type { get; set; }

    public DynamicFieldLabel Label { get; set; } = new();

    public List<string> Validators { get; set; } = new();
}