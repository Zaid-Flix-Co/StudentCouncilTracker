namespace StudentCouncilTracker.Web.Services.UserProviders;

public interface IUserProvider
{
    string Name { get; set; }

    void ParseJwt(string token);
}