using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Users.Domain;

namespace StudentCouncilTracker.Application.Entities.Interfaces;

public interface IStudentCouncilTrackerDbContext
{
    DbSet<CatalogUser> CatalogUsers { get; set; }
}