using application_recip.Constants;
using application_recip.Services.IngredientsService;
using application_recip.Store.BaseStore;
using application_recip.Store.BaseStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.IngredientsStore;

public class IngredientsEffect(IIngredientsService recipsService,
    NavigationManager navigationManager)
    : BaseEffect<IngredientModel>(recipsService)
{
    [EffectMethod]
    public virtual async Task HandleCreateItemResultAction(CreateItemResultAction<IngredientModel> createItemResultAction, IDispatcher dispatcher)
    {
        if (createItemResultAction.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.IngredientsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleUpdateItemResultAction(UpdateItemResultAction<IngredientModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.IngredientsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleDeleteItemResultAction(DeleteItemResultAction<IngredientModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.IngredientsPath);
    }
}
