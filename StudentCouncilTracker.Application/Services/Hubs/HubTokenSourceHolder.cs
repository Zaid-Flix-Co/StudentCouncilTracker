using System.Collections.Concurrent;

namespace StudentCouncilTracker.Application.Services.Hubs;

/// <summary>
/// Держатель TS
/// </summary>
public static class HubTokenSourceHolder
{
    /// <summary>
    /// The token sources
    /// </summary>
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> TokenSources = new();

    /// <summary>
    /// Создать новый TS
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>CancellationTokenSource.</returns>
    public static CancellationTokenSource CreateTokenSource(string key)
    {
        RemoveTokenSource(key);

        var newts = new CancellationTokenSource();
        TokenSources.TryAdd(key, newts);
        return newts;
    }

    /// <summary>
    /// Удалить TCS
    /// </summary>
    /// <param name="key">The key.</param>
    public static void RemoveTokenSource(string key)
    {
        TokenSources.TryRemove(key, out var token);
        token?.Dispose();
    }

    /// <summary>
    /// Получить TS
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>CancellationTokenSource.</returns>
    public static CancellationTokenSource GetTokenSource(string key)
    {
        TokenSources.TryGetValue(key, out var ts);
        return ts ?? new CancellationTokenSource();
    }
}