using application_recip.Store.MessageStore.Actions;
using Fluxor;

namespace application_recip.Store.MessageStore;

public static class NotificationsReducers
{
    #region GetNotificationResultAction
    [ReducerMethod]
    public static NotificationsState ReduceGetNotificationResultAction(NotificationsState state, GetNotificationResultAction action) => new NotificationsState(currentState: state, notifications: action.Notifications);

    #endregion
}
