namespace StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;

public class JournalPermission
{
    public bool Create { get; set; }
    
    public bool CanPrint { get; set; }
    
    public bool CanChangePrintSetting { get; set; }
}