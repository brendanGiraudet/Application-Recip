using application_recip.Store.BaseStore.Actions;
using application_recip.Store.RecipsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Components.Pages.Recips;

public partial class Recips
{
    [Inject] public IState<RecipsState> RecipsState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetDatagridItemsAction<RecipModel>(new()));
    }
}
