using application_recip.Helpers;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.SaveBaseStore.Actions;
using Fluxor;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.RecipCategoriesStore;

public static class RecipCategoriesReducers
{
    #region GetItemsResultAction
    [ReducerMethod]
    public static RecipCategoriesState ReduceGetItemsResultAction(RecipCategoriesState state, GetItemsResultAction<RecipCategoryModel> action) => new RecipCategoriesState(currentState: state, itemsToSave: action.Items, actualItemsToSave: CloneHelper<RecipCategoryModel>.CloneEnumerable(action.Items));

    #endregion

    #region AddItemToSaveAction
    [ReducerMethod]
    public static RecipCategoriesState ReduceAddItemToSaveAction(RecipCategoriesState state, AddItemToSaveAction<RecipCategoryModel> action) => new RecipCategoriesState(currentState: state, itemsToSave: state.ExpectedItemsToSave.Append(action.Item));
    #endregion

    #region RemoveItemToSaveAction
    [ReducerMethod]
    public static RecipCategoriesState ReduceRemoveItemToSaveAction(RecipCategoriesState state, RemoveItemToSaveAction<RecipCategoryModel> action) => new RecipCategoriesState(currentState: state, itemsToSave: state.ExpectedItemsToSave.Where( i => i.CategoryId != action.Item.CategoryId));
    #endregion
    
}
