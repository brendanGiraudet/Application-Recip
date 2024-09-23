using application_recip.Constants;
using application_recip.EqualityComparers;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.IngredientsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;

namespace application_recip.Components.Pages.IngredientForm;

public partial class IngredientForm
{
    [Inject] public required IState<IngredientsState> IngredientsState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required IUserInfoService UserInfoService { get; set; }

    [Parameter] public Guid? IngredientId { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (IngredientId is not null)
        {
            Dispatcher.Dispatch(new GetItemAction<IngredientModel>(IngredientId.Value));
        }
        else
        {
            Dispatcher.Dispatch(new InitializationAction<IngredientModel>(new IngredientModel()));
        }
    }

    private bool HaveChanges => !new IngredientEqualityComparer().Equals(IngredientsState.Value.ExpectedItem, IngredientsState.Value.ActualItem);

    void Submit(IngredientModel model)
    {
        if (IngredientId is null)
        {
            model.Id = Guid.NewGuid();

            Dispatcher.Dispatch(new CreateItemAction<IngredientModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.CreateIngredientRoutingKey));
        }
        else if (HaveChanges)
        {
            Dispatcher.Dispatch(new UpdateItemAction<IngredientModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.UpdateIngredientRoutingKey));
        }
    }


    void Cancel()
    {
        NavigationManager.NavigateTo(PageUrlsConstants.IngredientsPath);
    }

    void Delete()
    {
        Dispatcher.Dispatch(new DeleteItemAction<IngredientModel>(IngredientsState.Value.ExpectedItem, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.DeleteIngredientRoutingKey));
    }
}
