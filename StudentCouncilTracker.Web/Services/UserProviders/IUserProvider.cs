namespace StudentCouncilTracker.Web.Services.UserProviders;

public interface IUserProvider
{
    string UserId { get; set; }

    string Name { get; set; }

    void ParseJwt(string token);
}