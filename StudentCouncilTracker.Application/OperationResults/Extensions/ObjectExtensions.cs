namespace StudentCouncilTracker.Application.OperationResults.Extensions;

public static class ObjectExtensions
{
    public static OperationResult<TValue> ToOperationResult<TValue>(this TValue value)
    {
        return new OperationResult<TValue>()
            .SetValue(value);
    }
}