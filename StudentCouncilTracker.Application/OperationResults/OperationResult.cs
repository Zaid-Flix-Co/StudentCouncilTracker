using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

[Serializable]
public class OperationResult : OperationResultBase<OperationResult>
{
    public static OperationResult Create()
    {
        return new OperationResult();
    }

    public static OperationResult<TValue> CreateResult<TValue>(TValue value)
    {
        var result = new OperationResult<TValue>();
        result.SetValue(value);
        return result;
    }

    public static OperationResult CreateResult()
    {
        return new OperationResult();
    }

    public static OperationResult Merge(params OperationResult[] results)
    {
        return OperationResultHelper.Merge(results);
    }

    public static OperationResult<IEnumerable<TValue>> Merge<TValue>(params OperationResult<TValue>[] results)
    {
        return OperationResultHelper.MergeWithValue(results);
    }
}

[Serializable]
public class OperationResult<TValue> : OperationResultBase<OperationResult<TValue>>, IOperationResult<TValue>
{
    private TValue _value;

    public TValue Value
    {
        get => _value;
        set => _value = value;
    }

    public OperationResult<TValue> SetValue(TValue value)
    {
        Value = value;
        return this;
    }

    public static implicit operator OperationResult<TValue>(TValue value)
    {
        if (value is OperationResult<TValue> r)
            return r;

        return OperationResult.CreateResult(value);
    }

    public void Deconstruct(out bool ok, out TValue value)
    {
        ok = Ok;
        value = Ok ? Value : default;
    }
}