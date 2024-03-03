using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.OperationResults;
using System.ComponentModel.DataAnnotations;
using Softius_Extensions_NetStandart;
using StudentCouncilTracker.Application.Entities.Events.Dto;

namespace StudentCouncilTracker.Application.Entities.Events.Domain;

/// <summary>
/// Мероприятие
/// </summary>
public class Event : UserCuBase, IHaveId
{
    public int Id { get; set; }

    [Display(Name = "Наименование мероприятия")]
    public required string Name { get; set; }

    [Display(Name = "Описание мероприятия")]
    public string? Description { get; set; }

    public int? EventTypeId { get; set; }

    [Display(Name = "Тип мероприятия")]
    public virtual EventType? EventType { get; set; }

    public int? ResponsibleUserId { get; set; }

    [Display(Name = "Ответственный за мероприятие")]
    public virtual CatalogUser? ResponsibleUser { get; set; }

    [Display(Name = "Дата проведения мероприятия")]
    public DateTime? DateEvent { get; set; }

    [Display(Name = "Действующий")]
    public bool IsDeactivated { get; set; }

    public OperationResult Edit(EventDtoData data, string userName)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        Description = data.Description?.Value;
        EventTypeId = data.EventType?.Value.Id == 0 ? null : data.EventType?.Value.Id;
        ResponsibleUserId = data.ResponsibleUser?.Value.Id == 0 ? null : data.ResponsibleUser?.Value.Id;
        DateEvent = data.DateEvent?.Value;
        IsDeactivated = data.IsDeactivated!.Value;
        UpdatedDate = DateTime.Now;
        if(userName.IsNotEmpty())
            UpdatedUserName = userName;

        return operationResult;
    }
}