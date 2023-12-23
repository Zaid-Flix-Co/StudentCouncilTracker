namespace StudentCouncilTracker.Application.OperationResults;

internal static class OperationResultHelper
{
    public static OperationResult Merge(IEnumerable<OperationResultBase> results)
    {
        return OperationResult.CreateResult().AddReasons(results.SelectMany(result => result.Reasons));
    }

    public static OperationResult<IEnumerable<TValue>> MergeWithValue<TValue>(
        IEnumerable<OperationResult<TValue>> results)
    {
        var resultList = results.ToList();

        var finalResult = OperationResult.CreateResult<IEnumerable<TValue>>(new List<TValue>())
            .AddReasons(resultList.SelectMany(result => result.Reasons));

        if (finalResult.Ok)
            finalResult.SetValue(resultList.Select(r => r.Value).ToList());

        return finalResult;
    }
}