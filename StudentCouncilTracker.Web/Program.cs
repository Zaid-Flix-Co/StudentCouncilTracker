using Microsoft.AspNetCore.HttpOverrides;
using StudentCouncilTracker.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();

builder.Services.AddWeb();

builder.Services.AddHttpClient("StudentCouncilTrackerWebApi", client =>
{
    #if(DEBUG)
    client.BaseAddress = new Uri(builder.Configuration["APP_API_DEBUG"]!);
    #elif (RELEASE)
    client.BaseAddress = new Uri(builder.Configuration["APP_API_RELEASE"]!);
    #endif
    client.Timeout = TimeSpan.FromMinutes(15);
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();