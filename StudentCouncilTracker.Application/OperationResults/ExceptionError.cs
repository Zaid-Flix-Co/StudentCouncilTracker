using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

public class ExceptionalError : Error, IExceptionalError
{
    public ExceptionalError(Exception exception)
        : this(exception.Message, exception)
    {
    }

    public ExceptionalError(string message, Exception exception)
        : base(message)
    {
        Exception = exception;
    }

    public Exception Exception { get; }
}