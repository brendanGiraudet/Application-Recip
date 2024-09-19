using application_recip.Stores.BaseStore.Actions;
using application_recip.Stores.RecipsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Components.Pages.RecipForm;

public partial class RecipForm
{
    [Inject] public required IState<RecipsState> RecipsState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }

    RecipModel _actualRecip = new RecipModel();

    void Submit(RecipModel model)
    {
        Dispatcher.Dispatch(new CreateItemAction<RecipModel>(model));
    }

    void Cancel()
    {
        // redirect to list
    }
}
