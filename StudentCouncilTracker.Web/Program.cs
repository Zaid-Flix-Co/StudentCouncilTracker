using StudentCouncilTracker.Web.Services.Catalogs.Auth;
using StudentCouncilTracker.Web.Services.Catalogs.EventActions;
using StudentCouncilTracker.Web.Services.Catalogs.EventActionStatuses;
using StudentCouncilTracker.Web.Services.Catalogs.EventActionTypes;
using StudentCouncilTracker.Web.Services.Catalogs.Events;
using StudentCouncilTracker.Web.Services.Catalogs.EventTypes;
using StudentCouncilTracker.Web.Services.Catalogs.UserRoles;
using StudentCouncilTracker.Web.Services.Catalogs.Users;
using StudentCouncilTracker.Web.Services.UserProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<EventActionService>();
builder.Services.AddScoped<EventTypeService>();
builder.Services.AddScoped<EventActionTypeService>();
builder.Services.AddScoped<EventActionStatusService>();
builder.Services.AddScoped<CatalogUserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<IUserProvider, UserProvider>();

builder.Services.AddHttpClient("StudentCouncilTrackerWebApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APP_API"]!);
    client.Timeout = TimeSpan.FromMinutes(15);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
