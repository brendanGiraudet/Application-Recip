using application_recip.Constants;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.MessageStore;
using application_recip.Store.NotificationsStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_notification.Ms_notification.Models;

namespace application_recip.Components.Pages.Notifications;

public partial class Notifications
{
    [Inject] public IState<NotificationsState> NotificationsState { get; set; }

    [Inject] public IDispatcher Dispatcher { get; set; }

    [Inject] public IUserInfoService UserInfoService { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetNotificationsAction());
    }

    void Delete(NotificationModel notificationModel)
    {
        Dispatcher.Dispatch(new DeleteItemAction<NotificationModel>(notificationModel, RabbitmqConstants.NotifiactionExchangeName, RabbitmqConstants.DeleteNotificationRoutingKey));
    }
}
