using application_recip.Store.BaseStore;
using Fluxor;
using ms_notification.Ms_notification.Models;
using Radzen;

namespace application_recip.Store.MessageStore;

[FeatureState]
public class NotificationsState : BaseState<NotificationModel>
{
    private NotificationsState()
    {

    }

    public NotificationsState(
        NotificationsState? currentState = null,
        ODataEnumerable<NotificationModel>? datagridItems = null,
        int? totalItems = null,
        NotificationModel? expectedItem = default
        )
        : base(currentState, datagridItems, totalItems, expectedItem) { }
}
