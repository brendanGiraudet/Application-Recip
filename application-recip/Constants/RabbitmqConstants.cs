namespace application_recip.Constants;

public static class RabbitmqConstants
{
    public const string ApplicationName = "application-recip";

    // EXCHANGES
    public const string RecipExchangeName = "recip";
    public const string NotifiactionExchangeName = "notification";

    // RECIP ROUTING KEY
    public const string CreateRecipRoutingKey = "CreateRecip";
    
    public const string UpdateRecipRoutingKey = "UpdateRecip";
    
    public const string DeleteRecipRoutingKey = "DeleteRecip";
    
    // NOTIFICATION ROUTING KEY
    public const string DeleteNotificationRoutingKey = "DeleteNotification";

    // INGREDIENT ROUTING KEY
    public const string CreateIngredientRoutingKey = "CreateIngredient";

    public const string UpdateIngredientRoutingKey = "UpdateIngredient";

    public const string DeleteIngredientRoutingKey = "DeleteIngredient";
    
    // CATEGORY ROUTING KEY
    public const string CreateCategoryRoutingKey = "CreateCategory";

    public const string UpdateCategoryRoutingKey = "UpdateCategory";

    public const string DeleteCategoryRoutingKey = "DeleteCategory";

    // PROFIL ROUTING KEY
    public const string CreateProfilRoutingKey = "CreateProfil";

    public const string UpdateProfilRoutingKey = "UpdateProfil";

    public const string DeleteProfilRoutingKey = "DeleteProfil";
}
