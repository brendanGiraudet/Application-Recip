using application_recip.Constants;
using application_recip.EqualityComparers;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.ProfilsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using application_recip.Store.GetBaseStore.Actions;

namespace application_recip.Components.Pages.ProfilForm;

public partial class ProfilForm
{
    [Inject] public required IState<ProfilsState> ProfilsState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required IUserInfoService UserInfoService { get; set; }

    [Parameter] public Guid? ProfilId { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (ProfilId is not null)
        {
            Dispatcher.Dispatch(new GetItemAction<ProfilModel>(ProfilId.Value));
        }
        else
        {
            Dispatcher.Dispatch(new InitializationAction<ProfilModel>(new ProfilModel()));
        }
    }

    private bool HaveChanges => !new ProfilEqualityComparer().Equals(ProfilsState.Value.ExpectedItem, ProfilsState.Value.ActualItem);

    void Submit(ProfilModel model)
    {
        if (ProfilId is null)
        {
            model.Id = Guid.NewGuid();

            Dispatcher.Dispatch(new CreateItemAction<ProfilModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.CreateProfilRoutingKey));
        }
        else if (HaveChanges)
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
