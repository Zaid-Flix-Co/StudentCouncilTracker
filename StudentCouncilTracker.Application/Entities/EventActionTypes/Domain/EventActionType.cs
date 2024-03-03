using Softius_Extensions_NetStandart;
using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;

public class EventActionType : UserCuBase, IHaveId
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public OperationResult Edit(EventActionTypeDtoData data, string userName)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        UpdatedDate = DateTime.Now;

        if (userName.IsNotEmpty())
            UpdatedUserName = userName;

        return operationResult;
    }
}