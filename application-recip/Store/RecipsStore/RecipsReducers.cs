using application_recip.Store.BaseStore.Actions;
using Fluxor;
using ms_recip.Ms_recip.Models;

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
}
