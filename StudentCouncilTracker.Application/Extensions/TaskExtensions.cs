using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Extensions;

public static class TaskExtensions
{
    public static async Task<OperationResult<object>> WhenAllExtended(this ICollection<Task<OperationResult<object>>> tasks, CancellationToken token, Action onProgressStep)
    {
        var errors = new List<string>();
        var results = new List<string>();
        var ret = OperationResult.CreateResult<object>(false);
        var bigTask = Task.WhenAll(tasks);
        while (!bigTask.IsCompleted)
        {
            token.ThrowIfCancellationRequested();
            onProgressStep();
            await Task.WhenAny(bigTask, Task.Delay(500, token));
        }

        foreach (var tres in tasks.Select(s => s.Result))
        {
            if (!tres.Ok)
                errors.Add($"{tres.Value}: {string.Join(", ",tres.Reasons.FindAll(f => f.Type == ReasonType.Error).Select(s => s.Message))}");

            results.Add($"result: {tres.Value}, success: {tres.Ok}, errors: {string.Join(", ", tres.Reasons.FindAll(f => f.Type == ReasonType.Error).Select(s => s.Message))}");
        }

        ret.SetValue(results);
        if (errors.Any())
            ret.AddError(string.Join("<br/>", errors));

        return ret;
    }
}