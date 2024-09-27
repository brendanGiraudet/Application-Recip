using application_recip.Store.BaseStore.Actions;
using application_recip.Store.GetBaseStore.Actions;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.ProfilsStore;

public static class ProfilsReducers
{
    #region GetDatagridItemsResultAction
    [ReducerMethod]
    public static ProfilsState ReduceGetDatagridItemsResultAction(ProfilsState state, GetDatagridItemsResultAction<ProfilModel> action) => new ProfilsState(currentState: state, datagridItems: action.Items);

    #endregion

    #region GetItemAction
    [ReducerMethod]
    public static ProfilsState ReduceGetItemResultAction(ProfilsState state, GetItemResultAction<ProfilModel> action) => new ProfilsState(currentState: state, expectedItem: action.Item);

    #endregion

    #region InitializationAction
    [ReducerMethod]
    public static ProfilsState ReduceInitializationAction(ProfilsState state, InitializationAction<ProfilModel> action) => new ProfilsState(currentState: state, expectedItem: action.Item);
    #endregion

    #region CreateItemResultAction
    [ReducerMethod]
    public static ProfilsState ReduceCreateItemResultAction(ProfilsState state, CreateItemResultAction<ProfilModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Append(action.ItemToCreate);
        }

        return new ProfilsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
    
    #region UpdateItemResultAction
    [ReducerMethod]
    public static ProfilsState ReduceUpdateItemResultAction(ProfilsState state, UpdateItemResultAction<ProfilModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);

            datagridItems = datagridItems.Append(action.Item);
        }

        return new ProfilsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion

    #region DeleteItemResultAction
    [ReducerMethod]
    public static ProfilsState ReduceDeleteItemResultAction(ProfilsState state, DeleteItemResultAction<ProfilModel> action)
    {
        var datagridItems = state.Items.AsEnumerable();

        if (action.IsSuccess)
        {
            datagridItems = datagridItems.Where(r => r.Id != action.Item.Id);
        }

        return new ProfilsState(currentState: state, datagridItems: datagridItems.AsODataEnumerable());
    }

    #endregion
}
