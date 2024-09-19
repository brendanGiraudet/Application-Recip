using application_recip.Models;
using ms_notification.Ms_notification.Models;

namespace application_recip.Services.NotificationsService;

public interface INotificationsService
{
    Task<MethodResult<IEnumerable<NotificationModel>>> GetNotificationsAsync();
}
