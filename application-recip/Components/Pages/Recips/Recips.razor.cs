using application_recip.Constants;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.RecipsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Components.Pages.Recips;

public partial class Recips
{
    [Inject] public required IState<RecipsState> RecipsState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetDatagridItemsAction<RecipModel>(new()));
    }

    private void RedirectToRecipFormPage(Guid? id = null) => NavigationManager.NavigateTo(PageUrlsConstants.GetRecipFormPath(id));
}
