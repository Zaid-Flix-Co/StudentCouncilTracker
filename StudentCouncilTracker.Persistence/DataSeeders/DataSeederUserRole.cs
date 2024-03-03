using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;

namespace StudentCouncilTracker.Persistence.DataSeeders;

public static class DataSeederUserRole
{
    public static ModelBuilder SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = (int)Role.Chairman, Name = "Председатель" });
        modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = (int)Role.ViceСhairman, Name = "Заместитель председателя" });
        modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = (int)Role.CulturalOrganizer, Name = "Культурный организатор" });
        modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = (int)Role.HeadOf, Name = "Руководитель направления" });
        modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = (int)Role.Member, Name = "Член студенческого совета" });

        return modelBuilder;
    }
}