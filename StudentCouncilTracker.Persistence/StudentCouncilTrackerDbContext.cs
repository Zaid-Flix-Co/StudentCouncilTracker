using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Persistence.DataSeeders;

namespace StudentCouncilTracker.Persistence;

public class StudentCouncilTrackerDbContext : DbContext, IStudentCouncilTrackerDbContext, IDataProtectionKeyContext
{
    public StudentCouncilTrackerDbContext(DbContextOptions<StudentCouncilTrackerDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DataSeederEventType.SeedData(modelBuilder);
    }

    public DbSet<DataProtectionKey> DataProtectionKeys => null!;

    public DbSet<CatalogUser> CatalogUsers { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<EventType> EventTypes { get; set; } = null!;

    public DbSet<EventAction> EventActions { get; set; } = null!;

    public DbSet<EventActionType> EventActionTypes { get; set; } = null!;
}