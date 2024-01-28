using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;

namespace StudentCouncilTracker.Persistence.DataSeeders;

public static class DataSeederEventType
{
    public static ModelBuilder SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>().HasData(new EventType { Id = 1, Name = "Концерт" });
        modelBuilder.Entity<EventType>().HasData(new EventType { Id = 2, Name = "Малое мероприятие" });
        modelBuilder.Entity<EventType>().HasData(new EventType { Id = 3, Name = "Среднее мероприятие" });
        modelBuilder.Entity<EventType>().HasData(new EventType { Id = 4, Name = "Большое мероприятие" });

        return modelBuilder;
    }
}