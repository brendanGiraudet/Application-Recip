using application_recip.Components;
using application_recip.Extensions;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Authentication;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Authentication duende
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["DuendeLogin:Authority"];
        options.ClientId = builder.Configuration["DuendeLogin:ClientId"];
        options.ClientSecret = builder.Configuration["DuendeLogin:ClientSecret"];
        options.ResponseType = "code";

        var scopes = builder.Configuration["DuendeLogin:Scopes"].Split(',');

        options.Scope.Clear();
        foreach (var scope in scopes)
        {
            options.Scope.Add(scope);
        }

        options.GetClaimsFromUserInfoEndpoint = true;

        var claims = builder.Configuration["DuendeLogin:CustomClaims"].Split(',');

        foreach (var claim in claims)
        {
            options.ClaimActions.MapUniqueJsonKey(claim.Trim(), claim.Trim());
        }

        options.MapInboundClaims = false;

        options.SaveTokens = false;
    });

// Radzen
builder.Services.AddRadzenComponents();

// Add Fluxor
builder.Services.AddFluxor(config =>
{
    config
      .ScanAssemblies(typeof(Program).Assembly)
      .UseReduxDevTools();
});

builder.Services.AddLocalization();
builder.Services.AddHttpClient();


builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddCustomHostedServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .RequireAuthorization();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "fr-FR", "en-US" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

app.Run();
