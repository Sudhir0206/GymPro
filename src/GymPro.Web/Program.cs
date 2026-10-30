using GymPro.Web.Components;
using GymPro.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddHttpClient<GymApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7181/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
