namespace StudentCouncilTracker.Application.OperationResults.Interfaces;

public interface IOperationResultBase
{
    bool Ok { get; }

    /// <summary>
    /// Get all reasons (errors and successes)
    /// </summary>
    List<IReason> Reasons { get; }
}