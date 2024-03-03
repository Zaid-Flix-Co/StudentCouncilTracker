using StudentCouncilTracker.Application.DynamicFields;
using StudentCouncilTracker.Application.DynamicFields.Attributes;
using StudentCouncilTracker.Application.DynamicFields.Enums;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto.Combobox;

namespace StudentCouncilTracker.Application.Entities.Users.Dto;

public class CatalogUserDtoData : EntityDtoData, IHaveId
{
    public int Id { get; set; }

    [DynamicField(DynamicFieldType.Textbox)]
    public DynamicFieldValue<string>? Name { get; set; }

    [DynamicField(DynamicFieldType.Textbox)]
    public DynamicFieldValue<string>? Password { get; set; }

    [DynamicField(DynamicFieldType.Textbox)]
    public DynamicFieldValue<string>? PhoneNumber { get; set; }

    [DynamicField(DynamicFieldType.Textbox)]
    public DynamicFieldValue<string>? Email { get; set; }

    [DynamicField(DynamicFieldType.CheckBox)]
    public DynamicFieldValue<bool> IsDeactivated { get; set; }

    [DynamicField(DynamicFieldType.UserRole)]
    public DynamicFieldValue<UserRoleDtoCombobox>? Role { get; set; }
}