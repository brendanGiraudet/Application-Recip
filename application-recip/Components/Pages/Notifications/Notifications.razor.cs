using application_recip.Store.MessageStore.Actions;
using application_recip.Store.MessageStore;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace application_recip.Components.Pages.Notifications;

public partial class Notifications
{
    [Inject] public IState<NotificationsState> NotificationsState { get; set; }

    [Inject] public IDispatcher Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetNotificationAction());
    }

    private void OnClose()
    {
        // TODO dispatch for delete notif
    }
}
