using application_recip.Constants;
using application_recip.Services.RecipsService;
using application_recip.Store.BaseStore;
using application_recip.Store.BaseStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.RecipsStore;

public class RecipsEffect(IRecipsService recipsService,
    NavigationManager navigationManager)
    : BaseEffect<RecipModel>(recipsService)
{
    [EffectMethod]
    public virtual async Task HandleCreateItemResultAction(CreateItemResultAction<RecipModel> createItemResultAction, IDispatcher dispatcher)
    {
        if (createItemResultAction.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.RecipsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleUpdateItemResultAction(UpdateItemResultAction<RecipModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.RecipsPath);
    }
}
