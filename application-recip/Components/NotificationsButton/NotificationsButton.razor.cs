using application_recip.Constants;
using application_recip.HostedServices.NotificationsHostedService;
using application_recip.Store.MessageStore;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace application_recip.Components.NotificationsButton;

public partial class NotificationsButton
{
    [Inject] public IState<NotificationsState> NotificationsState { get; set; }

    [Inject] public IDispatcher Dispatcher { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public NotificationsHostedService NotificationsHostedservice { get; set; }

    private bool HaveNotifications() => NotificationsState.Value.Items.Count() > 0;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await NotificationsHostedservice.StartAsync(new CancellationToken());
    }

    private void OnClick()
    {
        NavigationManager.NavigateTo(PageUrlsConstants.NotificationsPath);
    }

    private string GetNotificationIcon() => HaveNotifications() ? MaterialIconConstants.ActiveNotification : MaterialIconConstants.Notification;
}
