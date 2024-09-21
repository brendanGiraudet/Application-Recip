using application_recip.Constants;
using application_recip.Services.CategoriesService;
using application_recip.Store.BaseStore;
using application_recip.Store.BaseStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.CategoriesStore;

public class CategoriesEffect(ICategoriesService recipsService,
    NavigationManager navigationManager)
    : BaseEffect<CategoryModel>(recipsService)
{
    [EffectMethod]
    public virtual async Task HandleCreateItemResultAction(CreateItemResultAction<CategoryModel> createItemResultAction, IDispatcher dispatcher)
    {
        if (createItemResultAction.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.CategoriesPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleUpdateItemResultAction(UpdateItemResultAction<CategoryModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.CategoriesPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleDeleteItemResultAction(DeleteItemResultAction<CategoryModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.CategoriesPath);
    }
}
