using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Enums;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Entities.EventActions.Domain;

/// <summary>
/// Задачи мероприятия
/// </summary>
public class EventAction : UserCuBase, IHaveId
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime? DeadlineCompletion { get; set; }

    public int? ResponsibleManagerId { get; set; }

    public virtual CatalogUser? ResponsibleManager { get; set; }

    public EventActionStatus Status { get; set; } = EventActionStatus.None;

    public int? EventActionTypeId { get; set; }

    public virtual EventActionType? EventActionType { get; set; }

    public OperationResult Edit(EventActionDtoData data)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        DeadlineCompletion = data.DeadlineCompletion?.Value;
        ResponsibleManagerId = data.ResponsibleManager?.Value.Id;
        EventActionTypeId = data.EventActionType?.Value.Id;
        UpdatedDate = DateTime.Now;

        return operationResult;
    }
}