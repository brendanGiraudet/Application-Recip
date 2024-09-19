using application_recip.Services.NotificationsService;
using application_recip.Store.MessageStore.Actions;
using Fluxor;

namespace application_recip.Store.BaseStore;

public class NotificationsEffect(INotificationsService _notificationsService)
{
    [EffectMethod]
    public async Task HandleGetNotificationAction(GetNotificationAction action, IDispatcher dispatcher)
    {
        var result = await _notificationsService.GetNotificationsAsync();

        if (result.IsSuccess) dispatcher.Dispatch(new GetNotificationResultAction(result.Value));
    }
}
