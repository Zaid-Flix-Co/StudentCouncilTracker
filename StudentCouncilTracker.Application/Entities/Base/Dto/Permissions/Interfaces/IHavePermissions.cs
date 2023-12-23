namespace StudentCouncilTracker.Application.Entities.Base.Dto.Permissions.Interfaces;

public interface IHavePermissions<T>
{
    T Permissions { get; set; }
}

public interface IHavePermissions : IHavePermissions<Permission>
{

}

#region Journal

public interface IHaveJournalPermissions : IHaveJournalPermissions<JournalPermission>
{
}
#endregion