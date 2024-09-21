using application_recip.Constants;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.IngredientsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Components.Pages.Ingredients;

public partial class Ingredients
{
    [Inject] public required IState<IngredientsState> IngredientsState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private void SearchIngredients(string searchTerm)
    {
        var loadArgs = new LoadDataArgs();

        loadArgs.Filter = $"contains(tolower({nameof(IngredientModel.Name)}), tolower('{searchTerm}'))";

        Dispatcher.Dispatch(new GetDatagridItemsAction<IngredientModel>(loadArgs));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetDatagridItemsAction<IngredientModel>(new()));
    }

    private void RedirectToIngredientFormPage(Guid? id = null) => NavigationManager.NavigateTo(PageUrlsConstants.GetIngredientFormPath(id));
}
