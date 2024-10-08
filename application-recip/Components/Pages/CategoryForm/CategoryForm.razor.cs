﻿using application_recip.Constants;
using application_recip.EqualityComparers;
using application_recip.Services.UserInfoService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.CategoriesStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using application_recip.Store.GetBaseStore.Actions;

namespace application_recip.Components.Pages.CategoryForm;

public partial class CategoryForm
{
    [Inject] public required IState<CategoriesState> CategoriesState { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required IUserInfoService UserInfoService { get; set; }

    [Parameter] public Guid? CategoryId { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (CategoryId is not null)
        {
            Dispatcher.Dispatch(new GetItemAction<CategoryModel>(CategoryId.Value));
        }
        else
        {
            Dispatcher.Dispatch(new InitializationAction<CategoryModel>(new CategoryModel()));
        }
    }

    private bool HaveChanges => !new CategoryEqualityComparer().Equals(CategoriesState.Value.ExpectedItem, CategoriesState.Value.ActualItem);

    void Submit(CategoryModel model)
    {
        if (CategoryId is null)
        {
            model.Id = Guid.NewGuid();

            Dispatcher.Dispatch(new CreateItemAction<CategoryModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.CreateCategoryRoutingKey));
        }
        else if (HaveChanges)
        {
            Dispatcher.Dispatch(new UpdateItemAction<CategoryModel>(model, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.UpdateCategoryRoutingKey));
        }
    }

    void Cancel()
    {
        NavigationManager.NavigateTo(PageUrlsConstants.CategoriesPath);
    }

    void Delete()
    {
        Dispatcher.Dispatch(new DeleteItemAction<CategoryModel>(CategoriesState.Value.ExpectedItem, RabbitmqConstants.RecipExchangeName, RabbitmqConstants.DeleteCategoryRoutingKey));
    }
}
