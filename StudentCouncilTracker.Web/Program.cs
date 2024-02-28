using StudentCouncilTracker.Web.Services;

var builder = WebApplication.CreateBuilder(args);

#if (DEBUG)

    var apiUrl = builder.Configuration.GetSection("HttpClientSettings:DebugApiUrl").Value;

#elif (RELEASE)

    var apiUrl = builder.Configuration.GetSection("HttpClientSettings:ReleaseApiUrl").Value;

#endif

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient(sp => new HttpClient
{
    BaseAddress = new Uri(apiUrl!)
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
