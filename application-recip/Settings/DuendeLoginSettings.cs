namespace application_recip.Settings;

public class DuendeLoginSettings
{
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scopes { get; set; }
    public string CustomClaims { get; set; }
    public string LogoutPath { get; set; }
    public string GetLogoutUrl() => $"{Authority}{LogoutPath}";

}
