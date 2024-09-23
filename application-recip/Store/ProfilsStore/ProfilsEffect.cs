using application_recip.Constants;
using application_recip.Services.ProfilsService;
using application_recip.Store.BaseStore;
using application_recip.Store.BaseStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.ProfilsStore;

public class ProfilsEffect(IProfilsService recipsService,
    NavigationManager navigationManager)
    : BaseEffect<ProfilModel>(recipsService)
{
    [EffectMethod]
    public virtual async Task HandleCreateItemResultAction(CreateItemResultAction<ProfilModel> createItemResultAction, IDispatcher dispatcher)
    {
        if (createItemResultAction.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.ProfilsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleUpdateItemResultAction(UpdateItemResultAction<ProfilModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.ProfilsPath);
    }
    
    [EffectMethod]
    public virtual async Task HandleDeleteItemResultAction(DeleteItemResultAction<ProfilModel> action, IDispatcher dispatcher)
    {
        if (action.IsSuccess) navigationManager.NavigateTo(PageUrlsConstants.ProfilsPath);
    }
}
