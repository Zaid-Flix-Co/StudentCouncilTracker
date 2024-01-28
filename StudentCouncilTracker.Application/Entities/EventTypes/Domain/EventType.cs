using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Domain;

public class EventType : UserCuBase, IHaveId
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public OperationResult Edit(EventTypeDtoData data)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        UpdatedDate = DateTime.Now;

        return operationResult;
    }
}