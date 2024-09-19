namespace application_recip.Constants;

public static class PageUrlsConstants
{
    public const string HomePath = "/";
    public const string NotificationsPath = "/notifications";
    
    public const string RecipsPath = "/recips";
    public static string GetRecipFormPath(Guid? id = null) => id is null ? "/recips/add" : $"/recips/{id}";
}
