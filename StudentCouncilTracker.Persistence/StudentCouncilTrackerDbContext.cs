using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.Users.Domain;

namespace StudentCouncilTracker.Persistence;

public class StudentCouncilTrackerDbContext : DbContext, IStudentCouncilTrackerDbContext, IDataProtectionKeyContext
{
    public StudentCouncilTrackerDbContext(DbContextOptions<StudentCouncilTrackerDbContext> options) : base(options)
    {

    }

    public DbSet<DataProtectionKey> DataProtectionKeys => null!;

    public DbSet<CatalogUser> CatalogUsers { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<EventType> EventTypes { get; set; } = null!;
}