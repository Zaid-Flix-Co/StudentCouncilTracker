using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.EventActions.Enums;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Domain;

namespace StudentCouncilTracker.Persistence.DataSeeders;

public static class DataSeederEventActionStatus
{
    public static ModelBuilder SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventActionStatus>().HasData(new EventActionStatus { Id = (int)ActionStatus.AwaitingExecution, Name = "Ожидает выполнения" });
        modelBuilder.Entity<EventActionStatus>().HasData(new EventActionStatus { Id = (int)ActionStatus.AtWork, Name = "В работе" });
        modelBuilder.Entity<EventActionStatus>().HasData(new EventActionStatus { Id = (int)ActionStatus.OnChecking, Name = "На проверке" });
        modelBuilder.Entity<EventActionStatus>().HasData(new EventActionStatus { Id = (int)ActionStatus.Completed, Name = "Завершена" });

        return modelBuilder;
    }
}