namespace StudentCouncilTracker.Application.OperationResults.Interfaces;

public interface IOperationResult<out TValue> : IOperationResultBase
{
    /// <summary>
    /// Get the Value.
    /// </summary>
    TValue Value { get; }
}