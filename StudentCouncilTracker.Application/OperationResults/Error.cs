using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

public class Error : IError
{
    protected Error()
    {
        Metadata = [];
    }

    public Error(string message)
        : this()
    {
        Message = message;
    }

    public string Message { get; protected set;  } = null!;

    public Dictionary<string, object> Metadata { get; }

    public ReasonType Type => ReasonType.Error;
}