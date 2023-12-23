using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Interfaces;

namespace StudentCouncilTracker.Persistence;

public class StudentCouncilTrackerDbContext : DbContext, IStudentCouncilTrackerDbContext, IDataProtectionKeyContext
{
    public StudentCouncilTrackerDbContext(DbContextOptions<StudentCouncilTrackerDbContext> options) : base(options)
    {

    }

    public DbSet<DataProtectionKey> DataProtectionKeys => null!;
}