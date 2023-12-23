using Microsoft.AspNetCore.SignalR;

namespace StudentCouncilTracker.Application.Services.Hubs.Progress;

public class HubProgress : Hub<IHubProgress>
{
    public Task Cancel(string key)
    {
        var tokenSource = HubTokenSourceHolder.GetTokenSource(key);
        tokenSource.Cancel();
        return Task.CompletedTask;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}