﻿using application_recip.Enums;
using application_recip.Services.BaseService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.MessageStore.Actions;
using Fluxor;

namespace application_recip.Store.BaseStore;

public class BaseEffect<T>(IBaseService<T> _baseService) 
    : GetBaseEffect<T>(_baseService)
    where T : class
{

    [EffectMethod]
    public virtual async Task HandleCreateItemAction(CreateItemAction<T> itemCreationAction, IDispatcher dispatcher)
    {
        var itemCreationResult = await _baseService.CreateAsync(itemCreationAction.Item, itemCreationAction.ExchangeName, itemCreationAction.RoutingKey);

        var createdItem = itemCreationAction.Item;
        var messageType = MessageTypeEnum.Error;

        if (itemCreationResult.IsSuccess)
        {
            createdItem = itemCreationResult.Value;
            messageType = MessageTypeEnum.Success;
        }

        dispatcher.Dispatch(new CreateItemResultAction<T>(createdItem, itemCreationResult.IsSuccess));
        dispatcher.Dispatch(new SetMessageAction(itemCreationResult.Message ?? string.Empty, messageType));
    }

    [EffectMethod]
    public async virtual Task HandleUpdateItemAction(UpdateItemAction<T> updateItemAction, IDispatcher dispatcher)
    {
        var updateResult = await _baseService.UpdateAsync(updateItemAction.Item, updateItemAction.ExchangeName, updateItemAction.RoutingKey);

        var updatedItem = updateItemAction.Item;
        var messageType = MessageTypeEnum.Error;

        if (updateResult.IsSuccess)
        {
            updatedItem = updateResult.Value;
            messageType = MessageTypeEnum.Success;
        }

        dispatcher.Dispatch(new UpdateItemResultAction<T>(updatedItem, updateResult.IsSuccess));
        dispatcher.Dispatch(new SetMessageAction(updateResult.Message ?? string.Empty, messageType));
    }

    [EffectMethod]
    public async Task HandleDeleteItemAction(DeleteItemAction<T> deleteItemAction, IDispatcher dispatcher)
    {
        var deleteResult = await _baseService.DeleteAsync(deleteItemAction.Item, deleteItemAction.ExchangeName, deleteItemAction.RoutingKey);

        var deletedItem = deleteItemAction.Item;
        var messageType = MessageTypeEnum.Error;

        if (deleteResult.IsSuccess)
        {
            deletedItem = deleteResult.Value;
            messageType = MessageTypeEnum.Success;
        }

        dispatcher.Dispatch(new DeleteItemResultAction<T>(deletedItem, deleteResult.IsSuccess));
        dispatcher.Dispatch(new SetMessageAction(deleteResult.Message ?? string.Empty, messageType));
    }
}
