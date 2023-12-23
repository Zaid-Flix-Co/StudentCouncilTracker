using StudentCouncilTracker.Application.DynamicProperties.Attributes;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions.Interfaces;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Base.Dto;

public class EntityDto<TData> : IHavePermissions where TData : IHaveId, new()
{
    [DynamicData]
    public TData Data { get; set; } = new();

    public Permission Permissions { get; set; } = new();
}

public class EntityDto<TData, TPermission> : IHavePermissions<TPermission> where TData : IHaveId, new() where TPermission : new()
{
    [DynamicData]
    public TData Data { get; set; } = new();

    public TPermission Permissions { get; set; } = new();
}

public class EntityDtoData
{

}