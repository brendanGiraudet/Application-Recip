using application_recip.Constants;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.ProfilsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Components.Pages.ProfilForm;

public partial class ProfilForm
{
    [Inject] public required IState<ProfilsState> ProfilsState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    [Inject] public required IUserInfoService UserInfoService { get; set; }
    
    [Parameter] public Guid? ProfilId { get; set; }

    private ProfilModel _actualProfil = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if(ProfilId is not null)
        {
            Dispatcher.Dispatch(new GetItemAction<ProfilModel>(ProfilId.Value));
        }
        else
        {
            Dispatcher.Dispatch(new InitializationAction<ProfilModel>(new ProfilModel()));
        }
    }

    void Submit(ProfilModel model)
    {
        if(ProfilId is null)
        {
            ProfilsState.Value.ExpectedItem.Id = Guid.NewGuid();
            
            Dispatcher.Dispatch(new CreateItemAction<ProfilModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.CreateProfilRoutingKey));
        }
        else
        {
            Dispatcher.Dispatch(new UpdateItemAction<ProfilModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.UpdateProfilRoutingKey));
        }
    }

    void Cancel()
    {
        NavigationManager.NavigateTo(PageUrlsConstants.ProfilsPath);
    }

    void Delete()
    {
        Dispatcher.Dispatch(new DeleteItemAction<ProfilModel>(ProfilsState.Value.ExpectedItem, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.DeleteProfilRoutingKey));
    }
}
