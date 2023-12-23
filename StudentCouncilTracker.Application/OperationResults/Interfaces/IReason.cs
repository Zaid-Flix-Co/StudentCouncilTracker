namespace StudentCouncilTracker.Application.OperationResults.Interfaces;

public interface IReason
{
    string Message { get; }

    Dictionary<string, object> Metadata { get; }

    ReasonType Type { get; }
}