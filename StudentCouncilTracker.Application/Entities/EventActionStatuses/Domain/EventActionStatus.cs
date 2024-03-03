using Softius_Extensions_NetStandart;
using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;

public class EventActionStatus : UserCuBase, IHaveId
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public OperationResult Edit(EventActionStatusDtoData data, string userName)
    {
        var operationResult = OperationResult.CreateResult();

        if (data.Name.Value == string.Empty)
            operationResult.AddError("Not valid name");

        if (!operationResult.Ok)
            return operationResult;

        Name = data.Name.Value;
        UpdatedDate = DateTime.Now;

        if(userName.IsNotEmpty())
            UpdatedUserName = userName;

        return operationResult;
    }
}