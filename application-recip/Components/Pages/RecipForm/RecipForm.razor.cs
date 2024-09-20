using application_recip.Constants;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.RecipsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Components.Pages.RecipForm;

public partial class RecipForm
{
    [Inject] public required IState<RecipsState> RecipsState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    [Inject] public required IUserInfoService UserInfoService { get; set; }
    
    [Parameter] public Guid? RecipId { get; set; }

    private RecipModel _actualRecip = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if(RecipId is not null)
        {
            Dispatcher.Dispatch(new GetItemAction<RecipModel>(RecipId.Value));
        }
        else
        {
            Dispatcher.Dispatch(new InitializationAction<RecipModel>(new RecipModel()));
        }
    }

    void Submit(RecipModel model)
    {
        if(RecipId is null)
        {
            RecipsState.Value.ExpectedItem.Id = Guid.NewGuid();

            RecipsState.Value.ExpectedItem.Authorname = UserInfoService.GetUserName();
            RecipsState.Value.ExpectedItem.AuthorId = UserInfoService.GetUserId();
            
            Dispatcher.Dispatch(new CreateItemAction<RecipModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.CreateRecipRoutingKey));
        }
        else
        {
            Dispatcher.Dispatch(new UpdateItemAction<RecipModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.UpdateRecipRoutingKey));
        }
    }

    void Cancel()
    {
        NavigationManager.NavigateTo(PageUrlsConstants.RecipsPath);
    }

    void Delete()
    {
        Dispatcher.Dispatch(new DeleteItemAction<RecipModel>(RecipsState.Value.ExpectedItem, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.DeleteRecipRoutingKey));
    }
}
