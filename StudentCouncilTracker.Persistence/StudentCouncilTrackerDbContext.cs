using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Persistence.DataSeeders;

namespace StudentCouncilTracker.Persistence;

public class StudentCouncilTrackerDbContext(DbContextOptions<StudentCouncilTrackerDbContext> options)
    : DbContext(options), IStudentCouncilTrackerDbContext, IDataProtectionKeyContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DataSeederEventType.SeedData(modelBuilder);
        DataSeederEventActionStatus.SeedData(modelBuilder);
        DataSeederUserRole.SeedData(modelBuilder);
    }

    public DbSet<DataProtectionKey> DataProtectionKeys => null!;

    public DbSet<CatalogUser> CatalogUsers { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<EventType> EventTypes { get; set; } = null!;

    public DbSet<EventAction> EventActions { get; set; } = null!;

    public DbSet<EventActionType> EventActionTypes { get; set; } = null!;

    public DbSet<EventActionStatus> EventActionStatuses { get; set; } = null!;

    public DbSet<UserRole> UserRoles { get; set; } = null!;
}