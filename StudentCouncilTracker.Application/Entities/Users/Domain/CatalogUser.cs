using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
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

    public OperationResult Edit(CatalogUserDtoData data)
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

        return operationResult;
    }
}