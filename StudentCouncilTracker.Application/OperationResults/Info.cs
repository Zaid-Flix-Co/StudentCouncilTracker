using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

public class Info : IInfo
{
    protected Info()
    {
        Metadata = new Dictionary<string, object>();
    }

    public Info(string message) : this()
    {
        Message = message;
    }

    public string Message { get; }

    public Dictionary<string, object> Metadata { get; }

    public ReasonType Type => ReasonType.Info;
}