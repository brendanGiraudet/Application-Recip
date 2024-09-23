namespace application_recip.Constants;

public static class PageUrlsConstants
{
    public const string HomePath = "/";
    public const string NotificationsPath = "/notifications";
    
    // RECIP
    public const string RecipsPath = "/recips";
    public static string GetRecipFormPath(Guid? id = null) => id is null ? $"{RecipsPath}/add" : $"{RecipsPath}/{id}";

    // INGREDIENT
    public const string IngredientsPath = "/ingredients";
    public static string GetIngredientFormPath(Guid? id = null) => id is null ? $"{IngredientsPath}/add" : $"{IngredientsPath}/{id}";

    // CATEGORIES
    public const string CategoriesPath = "/categories";
    public static string GetCategoryFormPath(Guid? id = null) => id is null ? $"{CategoriesPath}/add" : $"{CategoriesPath}/{id}";

    // PROFIL
    public const string ProfilsPath = "/profils";
    public static string GetProfilFormPath(Guid? id = null) => id is null ? $"{ProfilsPath}/add" : $"{ProfilsPath}/{id}";
}
