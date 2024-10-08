﻿using application_recip.Constants;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.RecipsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Components.Pages.Recips;

public partial class Recips
{
    [Inject] public required IState<RecipsState> RecipsState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private void SearchRecips(string searchTerm)
    {
        var loadArgs = new LoadDataArgs();

        loadArgs.Filter = $"contains(tolower({nameof(RecipModel.Name)}), tolower('{searchTerm}'))";

        Dispatcher.Dispatch(new GetItemsAction<RecipModel>(loadArgs));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetItemsAction<RecipModel>(new()));
    }

    private void RedirectToRecipFormPage(Guid? id = null) => NavigationManager.NavigateTo(PageUrlsConstants.GetRecipFormPath(id));
}
