using StudentCouncilTracker.Web.Services.Catalogs.Events;
using StudentCouncilTracker.Web.Services.Catalogs.EventTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<EventTypeService>();

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
