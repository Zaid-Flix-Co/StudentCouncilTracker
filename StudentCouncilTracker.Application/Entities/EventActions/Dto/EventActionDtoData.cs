using StudentCouncilTracker.Application.DynamicFields;
using StudentCouncilTracker.Application.DynamicFields.Attributes;
using StudentCouncilTracker.Application.DynamicFields.Enums;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto.Combobox;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto.Combobox;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.Users.Dto.Combobox;

namespace StudentCouncilTracker.Application.Entities.EventActions.Dto;

public class EventActionDtoData : EntityDtoData, IHaveId
{
    public int Id { get; set; }

    [DynamicField(DynamicFieldType.Textbox)]
    public DynamicFieldValue<string> Name { get; set; } = null!;

    [DynamicField(DynamicFieldType.CatalogUser)]
    public DynamicFieldValue<CatalogUserDtoCombobox>? ResponsibleManager { get; set; }

    [DynamicField(DynamicFieldType.Date)] 
    public DynamicFieldValue<DateTime>? DeadlineCompletion { get; set; }

    [DynamicField(DynamicFieldType.EventActionType)]
    public DynamicFieldValue<EventActionTypeDtoCombobox>? EventActionType { get; set; }

    [DynamicField(DynamicFieldType.EventActionStatus)]
    public DynamicFieldValue<EventActionStatusDtoCombobox>? Status { get; set; }
}