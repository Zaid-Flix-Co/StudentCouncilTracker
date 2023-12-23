namespace StudentCouncilTracker.Application.OperationResults.Extensions;

public static class EnumerableExtensions
{
    public static OperationResult Merge(this IEnumerable<OperationResult> results)
    {
        return OperationResultHelper.Merge(results);
    }

    public static OperationResult<IEnumerable<TValue>> Merge<TValue>(this IEnumerable<OperationResult<TValue>> results)
    {
        return OperationResultHelper.MergeWithValue(results);
    }
}