using application_recip.Constants;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.CategoriesStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Components.Pages.Categories;

public partial class Categories
{
    [Inject] public required IState<CategoriesState> CategoriesState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private void SearchCategories(string searchTerm)
    {
        var loadArgs = new LoadDataArgs();

        loadArgs.Filter = $"contains(tolower({nameof(CategoryModel.Name)}), tolower('{searchTerm}'))";

        Dispatcher.Dispatch(new GetDatagridItemsAction<CategoryModel>(loadArgs));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetDatagridItemsAction<CategoryModel>(new()));
    }

    private void RedirectToCategoryFormPage(Guid? id = null) => NavigationManager.NavigateTo(PageUrlsConstants.GetCategoryFormPath(id));
}
