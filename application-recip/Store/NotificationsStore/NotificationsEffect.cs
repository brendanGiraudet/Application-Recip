using application_recip.Constants;
using application_recip.Services.NotificationsService;
using application_recip.Services.UserInfoService;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.NotificationsStore.Actions;
using Fluxor;
using ms_notification.Ms_notification.Models;
using Radzen;

namespace application_recip.Store.BaseStore;

public class NotificationsEffect(
    INotificationsService _notificationsService,
    IUserInfoService userInfoService) 
    
    : BaseEffect<NotificationModel>(_notificationsService)
{
    [EffectMethod]
    public virtual async Task HandleGetNotificationsAction(GetNotificationsAction action, IDispatcher dispatcher)
    {

        var loadArgs = new LoadDataArgs();
        loadArgs.Filter = $"{nameof(NotificationModel.UserId)} eq '{userInfoService.GetUserId()}' " +
                          $"and {nameof(NotificationModel.Deleted)} eq false " +
                          $"and {nameof(NotificationModel.ApplicationName)} eq '{RabbitmqConstants.ApplicationName}'";

        dispatcher.Dispatch(new GetItemsAction<NotificationModel>(loadArgs));
    }
}
