using application_recip.Store.BaseStore.Actions;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.RecipsStore;

public static class RecipsReducers
{
    #region GetDatagridItemsResultAction
    [ReducerMethod]
    public static RecipsState ReduceGetDatagridItemsResultAction(RecipsState state, GetDatagridItemsResultAction<RecipModel> action) => new RecipsState(currentState: state, datagridItems: action.Items);

    #endregion

    #region GetItemAction
    [ReducerMethod]
    public static RecipsState ReduceGetItemResultAction(RecipsState state, GetItemResultAction<RecipModel> action) => new RecipsState(currentState: state, expectedItem: action.Item);

    #endregion

    #region InitializationAction
    [ReducerMethod]
    public static RecipsState ReduceInitializationAction(RecipsState state, InitializationAction<RecipModel> action) => new RecipsState(currentState: state, expectedItem: action.Item);
    #endregion

    #region CreateItemResultAction
    [ReducerMethod]
    public static RecipsState ReduceCreateItemResultAction(RecipsState state, CreateItemResultAction<RecipModel> action)
    {
        var datagridItems = state.DatagridItems.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Append(action.ItemToCreate);
        }

        return new RecipsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
    
    #region UpdateItemResultAction
    [ReducerMethod]
    public static RecipsState ReduceUpdateItemResultAction(RecipsState state, UpdateItemResultAction<RecipModel> action)
    {
        var datagridItems = state.DatagridItems.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);

            datagridItems = datagridItems.Append(action.Item);
        }

        return new RecipsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion

    #region DeleteItemResultAction
    [ReducerMethod]
    public static RecipsState ReduceDeleteItemResultAction(RecipsState state, DeleteItemResultAction<RecipModel> action)
    {
        var datagridItems = state.DatagridItems.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);
        }

        return new RecipsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
}
