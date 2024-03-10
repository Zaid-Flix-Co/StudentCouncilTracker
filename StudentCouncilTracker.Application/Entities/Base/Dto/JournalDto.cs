using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;
using StudentCouncilTracker.Application.Entities.Base.Dto.Permissions.Interfaces;

namespace StudentCouncilTracker.Application.Entities.Base.Dto;

public interface IJournalDto<TItem, TPermission> : IHaveJournalPermissions<TPermission>
    where TItem : class
{
    public TPermission Permissions { get; set; }

    List<TItem> Items { get; set; }

    int TotalCount { get; set; }

    string QueryString { get; set; }
}

public abstract class JournalDto<TItem, TPermission> : IJournalDto<TItem, TPermission>
    where TItem : class
{
    public TPermission Permissions { get; set; } = default!;

    public List<TItem> Items { get; set; } = null!;

    public int TotalCount { get; set; }

    public string QueryString { get; set; } = null!;
}

public abstract class JournalDto<TItem> : IJournalDto<TItem, JournalPermission>
    where TItem : class
{
    public JournalPermission Permissions { get; set; } = null!;

    public List<TItem> Items { get; set; } = null!;

    public int TotalCount { get; set; }

    public string QueryString { get; set; } = null!;
}