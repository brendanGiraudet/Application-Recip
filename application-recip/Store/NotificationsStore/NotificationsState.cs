using Fluxor;
using ms_notification.Ms_notification.Models;

namespace application_recip.Store.MessageStore;

[FeatureState]
public record NotificationsState
{
    public IEnumerable<NotificationModel> Notifications { get; set; }

    private NotificationsState()
    {
        Notifications = [];
    }

    public NotificationsState(NotificationsState? currentState = null, IEnumerable<NotificationModel>? notifications = null)
    {
        Notifications = notifications ?? (currentState != null ? currentState.Notifications : []);
    }
}
