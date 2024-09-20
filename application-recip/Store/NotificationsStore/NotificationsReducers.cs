using application_recip.Store.BaseStore.Actions;
using Fluxor;
using ms_notification.Ms_notification.Models;

namespace application_recip.Store.MessageStore;

public static class NotificationsReducers
{
    #region GetDatagridItemsResultAction
    [ReducerMethod]
    public static NotificationsState ReduceGetDatagridItemsResultAction(NotificationsState state, GetDatagridItemsResultAction<NotificationModel> action) => new NotificationsState(currentState: state, datagridItems: action.Items);

    #endregion
}
