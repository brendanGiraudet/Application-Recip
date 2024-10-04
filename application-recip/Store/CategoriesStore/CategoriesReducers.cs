using application_recip.Store.BaseStore.Actions;
using application_recip.Store.GetBaseStore.Actions;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.CategoriesStore;

public static class CategoriesReducers
{
    #region GetDatagridItemsResultAction
    [ReducerMethod]
    public static CategoriesState ReduceGetDatagridItemsResultAction(CategoriesState state, GetItemsResultAction<CategoryModel> action) => new CategoriesState(currentState: state, datagridItems: action.Items);

    #endregion

    #region GetItemAction
    [ReducerMethod]
    public static CategoriesState ReduceGetItemResultAction(CategoriesState state, GetItemResultAction<CategoryModel> action) => new CategoriesState(currentState: state, expectedItem: action.Item);

    #endregion

    #region InitializationAction
    [ReducerMethod]
    public static CategoriesState ReduceInitializationAction(CategoriesState state, InitializationAction<CategoryModel> action) => new CategoriesState(currentState: state, expectedItem: action.Item);
    #endregion

    #region CreateItemResultAction
    [ReducerMethod]
    public static CategoriesState ReduceCreateItemResultAction(CategoriesState state, CreateItemResultAction<CategoryModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Append(action.ItemToCreate);
        }

        return new CategoriesState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
    
    #region UpdateItemResultAction
    [ReducerMethod]
    public static CategoriesState ReduceUpdateItemResultAction(CategoriesState state, UpdateItemResultAction<CategoryModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);

            datagridItems = datagridItems.Append(action.Item);
        }

        return new CategoriesState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion

    #region DeleteItemResultAction
    [ReducerMethod]
    public static CategoriesState ReduceDeleteItemResultAction(CategoriesState state, DeleteItemResultAction<CategoryModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);
        }

        return new CategoriesState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
}
