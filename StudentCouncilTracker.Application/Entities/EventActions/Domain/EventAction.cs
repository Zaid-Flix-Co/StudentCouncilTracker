using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.OperationResults;
using System.ComponentModel.DataAnnotations;
using Softius_Extensions_NetStandart;

namespace StudentCouncilTracker.Application.Entities.EventActions.Domain;

/// <summary>
/// Задачи мероприятия
/// </summary>
public class EventAction : UserCuBase, IHaveId
{
    public int Id { get; set; }

    [Display(Name = "Наименование задачи")]
    public required string Name { get; set; }

    [Display(Name = "Крайний срок выполнения задачи")]
    public DateTime? DeadlineCompletion { get; set; }

    public int? ResponsibleManagerId { get; set; }

    [Display(Name = "Ответственный за задачу")]
    public virtual CatalogUser? ResponsibleManager { get; set; }

    public int? StatusId { get; set; }

    [Display(Name = "Статус задачи")]
    public virtual EventActionStatus? Status { get; set; }

    public int? EventActionTypeId { get; set; }

    [Display(Name = "Тип задачи")]
    public virtual EventActionType? EventActionType { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; }

    public OperationResult Edit(EventActionDtoData data, string userName)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        DeadlineCompletion = data.DeadlineCompletion?.Value;
        StatusId = data.Status?.Value.Id == 0 ? null : data.Status?.Value.Id;
        ResponsibleManagerId = data.ResponsibleManager?.Value.Id == 0 ? null : data.ResponsibleManager?.Value.Id;
        EventActionTypeId = data.EventActionType?.Value.Id == 0 ? null : data.EventActionType?.Value.Id;
        UpdatedDate = DateTime.Now;

        if(userName.IsNotEmpty())
            UpdatedUserName = userName;

        return operationResult;
    }
}