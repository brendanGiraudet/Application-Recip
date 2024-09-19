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

    private readonly RecipModel _actualRecip = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _actualRecip.Id = Guid.NewGuid();
        
        _actualRecip.Authorname = UserInfoService.GetUserName();
        _actualRecip.AuthorId = UserInfoService.GetUserId();
    }

    void Submit(RecipModel model)
    {
        Dispatcher.Dispatch(new CreateItemAction<RecipModel>(model));
    }

    void Cancel()
    {
        // redirect to list
    }
}
