namespace StudentCouncilTracker.Application.Entities.Interfaces.Haves;

public interface IHaveCombobox : IHaveCombobox<int>
{
}

public interface IHaveCombobox<T> : IHaveId<T>
{
    string Name { get; set; }
}