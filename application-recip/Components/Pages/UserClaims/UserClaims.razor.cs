using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace application_recip.Components.Pages.UserClaims;

public partial class UserClaims
{
    private IEnumerable<Claim> claims = Enumerable.Empty<Claim>();

    [CascadingParameter]private Task<AuthenticationState>? AuthState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (AuthState == null)
        {
            return;
        }

        var authState = await AuthState;
        claims = authState.User.Claims;
    }
}
