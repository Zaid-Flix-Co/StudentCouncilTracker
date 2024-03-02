using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Enums;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.OperationResults;
using System.ComponentModel.DataAnnotations;
using StudentCouncilTracker.Application.Entities.Events.Domain;

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

    [Display(Name = "Статус задачи")]
    public EventActionStatus Status { get; set; } = EventActionStatus.None;

    public int? EventActionTypeId { get; set; }

    [Display(Name = "Тип задачи")]
    public virtual EventActionType? EventActionType { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; }

    public OperationResult Edit(EventActionDtoData data)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        DeadlineCompletion = data.DeadlineCompletion?.Value;
        ResponsibleManagerId = data.ResponsibleManager?.Value.Id == 0 ? null : data.ResponsibleManager?.Value.Id;
        EventActionTypeId = data.EventActionType?.Value.Id == 0 ? null : data.EventActionType?.Value.Id;
        UpdatedDate = DateTime.Now;

        return operationResult;
    }
}