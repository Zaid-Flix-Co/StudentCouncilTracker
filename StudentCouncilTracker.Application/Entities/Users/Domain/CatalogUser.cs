using System.ComponentModel.DataAnnotations;
using Softius_Extensions_NetStandart;
using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Entities.Users.Domain;

public class CatalogUser : UserCuBase, IHaveId
{
    public int Id { get; set; }

    [Display(Name = "Имя пользователя")]
    public string? Name { get; set; }

    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    [Display(Name = "Адрес электронной почты")]
    public string? Email { get; set; }

    [Display(Name = "Номер телефона")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Действующий")]
    public bool IsDeactivated { get; set; }

    public int? RoleId { get; set; }

    [Display(Name = "Роль")]
    public virtual UserRole? Role { get; set; }

    public OperationResult Edit(CatalogUserDtoData data, string userName)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name?.Value == string.Empty)
            operationResult.AddError("Not valid organization name");
        
        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name?.Value;
        Password = data.Password?.Value;
        PhoneNumber = data.PhoneNumber?.Value;
        Email = data.Email?.Value;
        IsDeactivated = data.IsDeactivated.Value;
        RoleId = data.Role is { Value.Id: 0 } ? null : data.Role?.Value.Id;

        if (userName.IsNotEmpty())
            UpdatedUserName = userName;

        return operationResult;
    }
}