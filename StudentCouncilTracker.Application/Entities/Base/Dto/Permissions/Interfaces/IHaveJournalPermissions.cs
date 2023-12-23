namespace StudentCouncilTracker.Application.Entities.Base.Dto.Permissions.Interfaces;

public interface IHaveJournalPermissions<T>
{
    public T Permissions { get; set; }
}