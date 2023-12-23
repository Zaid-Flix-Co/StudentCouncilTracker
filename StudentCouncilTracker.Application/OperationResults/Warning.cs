using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

public class Warning : IWarning
{
    protected Warning()
    {
        Metadata = new Dictionary<string, object>();
    }

    public Warning(string message) : this()
    {
        Message = message;
    }

    public string Message { get; }

    public Dictionary<string, object> Metadata { get; }

    public ReasonType Type => ReasonType.Warning;
}