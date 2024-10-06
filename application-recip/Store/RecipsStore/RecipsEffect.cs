using application_recip.Constants;
using application_recip.Services.RecipsService;
using application_recip.Store.BaseStore;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.RecipCategoriesStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.RecipsStore;

public class RecipsEffect(IRecipsService RecipsService,
    NavigationManager NavigationManager, IState<RecipCategoriesState> RecipCategoriesState)
    : BaseEffect<RecipModel>(RecipsService)
{
    [EffectMethod]
    public virtual async Task HandleCreateItemAction(CreateItemAction<RecipModel> action, IDispatcher dispatcher)
    {
        var categories = RecipCategoriesState.Value.ExpectedItemsToSave;

        action.Item.Categories = new(categories);

        base.HandleCreateItemAction(action, dispatcher);
    }

    [EffectMethod]
    public virtual async Task HandleUpdateItemAction(UpdateItemAction<RecipModel> action, IDispatcher dispatcher)
    {
        var categories = RecipCategoriesState.Value.ExpectedItemsToSave;

        action.Item.Categories = new(categories);

        base.HandleUpdateItemAction(action, dispatcher);
    }

    [EffectMethod]
    public virtual async Task HandleCreateItemResultAction(CreateItemResultAction<RecipModel> createItemResultAction, IDispatcher dispatcher)
    {
        if (createItemResultAction.IsSuccess) NavigationManager.NavigateTo(PageUrlsConstants.RecipsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleUpdateItemResultAction(UpdateItemResultAction<RecipModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) NavigationManager.NavigateTo(PageUrlsConstants.RecipsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleDeleteItemResultAction(DeleteItemResultAction<RecipModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) NavigationManager.NavigateTo(PageUrlsConstants.RecipsPath);
    }
}
