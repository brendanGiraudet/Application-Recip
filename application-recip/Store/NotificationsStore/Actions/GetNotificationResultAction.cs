using ms_notification.Ms_notification.Models;

namespace application_recip.Store.MessageStore.Actions;

public class GetNotificationResultAction
{
    public IEnumerable<NotificationModel> Notifications { get; }

    public GetNotificationResultAction(IEnumerable<NotificationModel> notifications)
    {
        Notifications = notifications;
    }
}
