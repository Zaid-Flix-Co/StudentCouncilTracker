namespace StudentCouncilTracker.Application.OperationResults.Interfaces;

public interface IExceptionalError : IError
{
    Exception Exception { get; }
}