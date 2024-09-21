using application_recip.Store.BaseStore.Actions;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.IngredientsStore;

public static class IngredientsReducers
{
    #region GetDatagridItemsResultAction
    [ReducerMethod]
    public static IngredientsState ReduceGetDatagridItemsResultAction(IngredientsState state, GetDatagridItemsResultAction<IngredientModel> action) => new IngredientsState(currentState: state, datagridItems: action.Items);

    #endregion

    #region GetItemAction
    [ReducerMethod]
    public static IngredientsState ReduceGetItemResultAction(IngredientsState state, GetItemResultAction<IngredientModel> action) => new IngredientsState(currentState: state, expectedItem: action.Item);

    #endregion

    #region InitializationAction
    [ReducerMethod]
    public static IngredientsState ReduceInitializationAction(IngredientsState state, InitializationAction<IngredientModel> action) => new IngredientsState(currentState: state, expectedItem: action.Item);
    #endregion

    #region CreateItemResultAction
    [ReducerMethod]
    public static IngredientsState ReduceCreateItemResultAction(IngredientsState state, CreateItemResultAction<IngredientModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Append(action.ItemToCreate);
        }

        return new IngredientsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
    
    #region UpdateItemResultAction
    [ReducerMethod]
    public static IngredientsState ReduceUpdateItemResultAction(IngredientsState state, UpdateItemResultAction<IngredientModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);

            datagridItems = datagridItems.Append(action.Item);
        }

        return new IngredientsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion

    #region DeleteItemResultAction
    [ReducerMethod]
    public static IngredientsState ReduceDeleteItemResultAction(IngredientsState state, DeleteItemResultAction<IngredientModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);
        }

        return new IngredientsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
}
