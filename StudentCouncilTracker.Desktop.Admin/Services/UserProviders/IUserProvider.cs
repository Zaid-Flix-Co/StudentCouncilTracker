﻿using StudentCouncilTracker.Application.Entities.UserRoles.Enums;

namespace StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

public interface IUserProvider
{
    string Name { get; set; }

    Role Role { get; set; }

    int UserId { get; set; }

    void ParseJwt(string token);
}