using application_recip.Store.SaveBaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.RecipCategoriesStore;

[FeatureState]
public class RecipCategoriesState : SaveBaseState<RecipCategoryModel>
{
    private RecipCategoriesState()
    {
        
    }

    public RecipCategoriesState(
        RecipCategoriesState? currentState = null,
        ODataEnumerable<RecipCategoryModel>? items = null,
        int? totalItems = null,
        RecipCategoryModel? expectedItem = default,
        IEnumerable<RecipCategoryModel>? itemsToSave = null,
        IEnumerable<RecipCategoryModel>? actualItemsToSave = null
        )
        : base (currentState, items, totalItems, expectedItem, itemsToSave, actualItemsToSave)
    {
        
    }
}
