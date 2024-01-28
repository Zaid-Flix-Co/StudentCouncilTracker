using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.Users.Domain;

namespace StudentCouncilTracker.Application.Entities.Interfaces;

public interface IStudentCouncilTrackerDbContext
{
    DbSet<CatalogUser> CatalogUsers { get; set; }

    DbSet<Event> Events { get; set; }

    DbSet<EventType> EventTypes { get; set; }
}