namespace StudentCouncilTracker.Application.Extensions;

public static class RandomExtensions
{
    private static readonly Random Random = new();

    public static string GetRandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}