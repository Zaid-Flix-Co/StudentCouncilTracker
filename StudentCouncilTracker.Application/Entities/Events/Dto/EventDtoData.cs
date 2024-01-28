using StudentCouncilTracker.Application.DynamicFields;
using StudentCouncilTracker.Application.DynamicFields.Attributes;
using StudentCouncilTracker.Application.DynamicFields.Enums;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto.Combobox;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.Users.Dto.Combobox;

namespace StudentCouncilTracker.Application.Entities.Events.Dto;

public class EventDtoData : EntityDtoData, IHaveId
{
    public int Id { get; set; }

    [DynamicField(DynamicFieldType.Textbox)]
    public DynamicFieldValue<string> Name { get; set; } = null!;

    [DynamicField(DynamicFieldType.TextArea)]
    public DynamicFieldValue<string>? Description { get; set; }

    [DynamicField(DynamicFieldType.EventType)]
    public DynamicFieldValue<EventTypeDtoCombobox>? EventType { get; set; }

    [DynamicField(DynamicFieldType.CatalogUser)]
    public DynamicFieldValue<CatalogUserDtoCombobox>? ResponsibleUser { get; set; }

    [DynamicField(DynamicFieldType.Date)]
    public DynamicFieldValue<DateTime>? DateEvent { get; set; }

    [DynamicField(DynamicFieldType.CheckBox)]
    public DynamicFieldValue<bool>? IsDeactivated { get; set; }
}