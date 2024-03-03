using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.EventActions.Domain;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.Users.Domain;

namespace StudentCouncilTracker.Application.Entities.Interfaces;

public interface IStudentCouncilTrackerDbContext
{
    DbSet<CatalogUser> CatalogUsers { get; set; }

    DbSet<Event> Events { get; set; }

    DbSet<EventType> EventTypes { get; set; }

    DbSet<EventAction> EventActions { get; set; }

    DbSet<EventActionType> EventActionTypes { get; set; }

    DbSet<EventActionStatus> EventActionStatuses { get; set; }

    DbSet<UserRole> UserRoles { get; set; }
}