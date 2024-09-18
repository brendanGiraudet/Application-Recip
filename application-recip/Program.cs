using application_recip.Components;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddLocalization();

// Radzen
builder.Services.AddRadzenComponents();

// Add Fluxor
builder.Services.AddFluxor(config =>
{
    config
      .ScanAssemblies(typeof(Program).Assembly)
      .UseReduxDevTools();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "fr-FR", "en-US" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

app.Run();
