namespace StudentCouncilTracker.Application.Entities.Interfaces.Haves;

public interface IHaveId : IHaveId<int>
{
}

public interface IHaveId<T>
{
    T Id { get; set; }
}