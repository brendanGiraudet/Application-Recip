using application_recip.Constants;
using application_recip.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace application_recip.Components.Pages.Logout;

public partial class Logout
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IOptions<DuendeLoginSettings> DuendeLoginSettingsOptions { get; set; }
    [Inject] public IHttpContextAccessor HttpContextAccessor { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContextAccessor.HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    }
}
