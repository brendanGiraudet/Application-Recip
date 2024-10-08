﻿using application_recip.Constants;
using application_recip.EqualityComparers;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.RecipsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.RecipCategoriesStore;
using Radzen;

namespace application_recip.Components.Pages.RecipForm;

public partial class RecipForm
{
    [Inject] public required IState<RecipsState> RecipsState { get; set; }

    [Inject] public required IState<RecipCategoriesState> RecipCategoriesState { get; set; }    

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    [Inject] public required IUserInfoService UserInfoService { get; set; }
    
    [Parameter] public Guid? RecipId { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if(RecipId is not null)
        {
            Dispatcher.Dispatch(new GetItemAction<RecipModel>(RecipId.Value));

            var loadArgs = new LoadDataArgs();
            loadArgs.Filter = $"{nameof(RecipCategoryModel.RecipId)} eq {RecipId}";

            Dispatcher.Dispatch(new GetItemsAction<RecipCategoryModel>(loadArgs));
        }
        else
        {
            Dispatcher.Dispatch(new InitializationAction<RecipModel>(new RecipModel()));
        }
    }

    private bool HaveChanges => !new RecipEqualityComparer().Equals(RecipsState.Value.ExpectedItem, RecipsState.Value.ActualItem)
    || !RecipCategoriesState.Value.ActualItemsToSave.SequenceEqual(RecipCategoriesState.Value.ExpectedItemsToSave, new RecipCategoryEqualityComparer());

    void Submit(RecipModel model)
    {
        if(RecipId is null)
        {
            model.Id = Guid.NewGuid();

            model.Authorname = UserInfoService.GetUserName();
            
            model.AuthorId = UserInfoService.GetUserId();
            
            Dispatcher.Dispatch(new CreateItemAction<RecipModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.CreateRecipRoutingKey));
        }
        else if (HaveChanges)
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
