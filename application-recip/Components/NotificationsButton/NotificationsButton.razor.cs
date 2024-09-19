﻿using application_recip.Constants;
using application_recip.Store.MessageStore;
using application_recip.Store.MessageStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace application_recip.Components.NotificationsButton;

public partial class NotificationsButton
{
    [Inject] public IState<NotificationsState> NotificationsState { get; set; }
    
    [Inject] public IDispatcher Dispatcher { get; set; }
    
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetNotificationAction());
    }

    private void OnClick()
    {
        NavigationManager.NavigateTo(PageUrlsConstants.NotificationsPath);
    }
}
