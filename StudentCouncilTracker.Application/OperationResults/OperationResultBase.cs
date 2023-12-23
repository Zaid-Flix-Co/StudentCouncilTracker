using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

[Serializable]
public abstract class OperationResultBase : IOperationResultBase
{
    protected OperationResultBase()
    {
        Reasons = new List<IReason>();
    }

    public bool Ok => !Reasons.OfType<IError>().Any();

    public List<IReason> Reasons { get; }
}

public abstract class OperationResultBase<TResult> : OperationResultBase where TResult : OperationResultBase<TResult>
{
    public TResult AddReason(IReason reason)
    {
        Reasons.Add(reason);
        return (TResult)this;
    }

    public TResult AddReasons(IEnumerable<IReason> reasons)
    {
        Reasons.AddRange(reasons);
        return (TResult)this;
    }

    public void AddError(string message)
    {
        AddReason(new Error(message));
    }

    public void AddError(Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception));

        AddReason(new ExceptionalError(exception.Message, exception));
    }

    public void AddError(string? message, Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception));

        AddReason(new ExceptionalError(message ?? exception.Message, exception));
    }

    public void AddSuccess(string message)
    {
        AddReason(new Success(message));
    }

    public void AddInfo(string message)
    {
        AddReason(new Info(message));
    }

    public void AddWarning(string message)
    {
        AddReason(new Warning(message));
    }
}