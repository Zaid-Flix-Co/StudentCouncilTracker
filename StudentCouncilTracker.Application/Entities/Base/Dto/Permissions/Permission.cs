namespace StudentCouncilTracker.Application.Entities.Base.Dto.Permissions;

public class Permission
{
    public bool Create { get; set; } = true;
    
    public bool Edit { get; set; } = true;
    
    public bool Delete { get; set; } = true;
}