using application_recip.Store.CategoriesStore;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.RecipCategoriesStore;
using application_recip.Store.SaveBaseStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Components.RecipCategorySelector;

public partial class RecipCategorySelector
{
    [Inject] public required IState<RecipCategoriesState> RecipCategoriesState { get; set; }

    [Inject] public required IState<CategoriesState> CategoriesState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Parameter] public Guid? RecipId { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if(RecipId is not null)
        {
            var loadDataArgs = new LoadDataArgs()
            {
                Filter = $"{nameof(RecipCategoryModel.RecipId)} eq {RecipId}"
            };

            Dispatcher.Dispatch(new GetItemsAction<RecipCategoryModel>(loadDataArgs));
        }

        Dispatcher.Dispatch(new GetItemsAction<CategoryModel>());
    }

    IEnumerable<CategoryModel> Categories => CategoriesState.Value.Items?.Where(c => !RecipCategoriesState.Value.ItemsToSave.Any(s => s.CategoryId == c.Id)) ?? [];

    IEnumerable<CategoryModel> SelectedCategories => CategoriesState.Value.Items?.Where(c => RecipCategoriesState.Value.ItemsToSave.Any(s => s.CategoryId == c.Id)) ?? [];

    private void OnCategorySelect(CategoryModel? selectedCategoryModel)
    {

        if(selectedCategoryModel is not null)
        {
            var recipCategory = new RecipCategoryModel{
                CategoryId = selectedCategoryModel.Id
            };

            Dispatcher.Dispatch(new AddItemToSaveAction<RecipCategoryModel>(recipCategory));
        }
    }

    private void OnCategoryRemove(CategoryModel removedCAtegoryModel)
    {
        var recipCategory = RecipCategoriesState.Value.ItemsToSave.FirstOrDefault(s => s.CategoryId == removedCAtegoryModel.Id);

        if(recipCategory is not null)
            Dispatcher.Dispatch(new RemoveItemToSaveAction<RecipCategoryModel>(recipCategory));
    }
}
