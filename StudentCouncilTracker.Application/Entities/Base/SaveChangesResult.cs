using StudentCouncilTracker.Application.Entities.Base.Interfaces;

namespace StudentCouncilTracker.Application.Entities.Base;

public class SaveChangesResult : ISaveChangesResult
{
    public Exception? Exception { get; set; }

    public bool IsOk => Exception == null;
}