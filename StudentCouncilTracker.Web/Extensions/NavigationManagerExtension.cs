using Microsoft.AspNetCore.Components;

namespace StudentCouncilTracker.Web.Extensions;

public static class NavigationManagerExtension
{
    private const string SubDomain = "skt";

    public static void NavigateToPage(this NavigationManager navigationManager, string url, bool forceLoad = false)
    {
        #if DEBUG
        navigationManager.NavigateTo(url, forceLoad);
        #elif RELEASE
        navigationManager.NavigateTo($"{SubDomain}/{skt}", forceLoad);
        #endif
    }
}