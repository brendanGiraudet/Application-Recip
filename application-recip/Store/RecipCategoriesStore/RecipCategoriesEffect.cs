using application_recip.Services.RecipCategoriesService;
using application_recip.Store.SaveBaseStore;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.RecipCategoriesStore;

public class RecipCategoriesEffect(IRecipCategoriesService recipCategoriesService) : SaveBaseEffect<RecipCategoryModel>(recipCategoriesService)
{

}
