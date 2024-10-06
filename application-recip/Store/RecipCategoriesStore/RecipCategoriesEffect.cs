using application_recip.Services.RecipCategoriesService;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.SaveBaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.RecipCategoriesStore;

public class RecipCategoriesEffect(IRecipCategoriesService RecipCategoriesService) : SaveBaseEffect<RecipCategoryModel>(RecipCategoriesService)
{
    [EffectMethod]
    public virtual async Task HandleGetItemResultAction(GetItemResultAction<RecipModel> action, IDispatcher dispatcher)
    {
        var loadDataArgs = new LoadDataArgs(){
            Filter = $"{nameof(RecipCategoryModel.RecipId)} eq {action.Item.Id}"
        };

        dispatcher.Dispatch(new GetItemsAction<RecipCategoryModel>(loadDataArgs));
    }
}