using StudentCouncilTracker.Application.OperationResults.Interfaces;

namespace StudentCouncilTracker.Application.OperationResults;

public class Success : ISuccess
{
    protected Success()
    {
        Metadata = new Dictionary<string, object>();
    }

    public Success(string message) : this()
    {
        Message = message;
    }

    public string Message { get; protected set; }

    public Dictionary<string, object> Metadata { get; }
        
    public ReasonType Type => ReasonType.Success;
}