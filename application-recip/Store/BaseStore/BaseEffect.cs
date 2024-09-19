using application_recip.Constants;
using application_recip.Enums;
using application_recip.Services.BaseService;
using application_recip.Store.BaseStore.Actions;
using application_recip.Store.MessageStore.Actions;
using Fluxor;
using Radzen;

namespace application_recip.Store.BaseStore;

public class BaseEffect<T>(IBaseService<T> _baseService) where T : class
{
    [EffectMethod]
    public virtual async Task HandleGetDatagridItemsAction(GetDatagridItemsAction<T> getItemsWithFilterAction, IDispatcher dispatcher)
    {
        var filteredItemsResult = await _baseService.GetDatagridItemsAsync(args: getItemsWithFilterAction.LoadDataArgs, expand: getItemsWithFilterAction.Expand, select: getItemsWithFilterAction.Select, count: getItemsWithFilterAction.Count);

        var items = filteredItemsResult.Value.AsODataEnumerable();
        var top = getItemsWithFilterAction.LoadDataArgs.Top ?? 10;

        dispatcher.Dispatch(new GetDatagridItemsResultAction<T>(items, filteredItemsResult.Count, top));
    }

    [EffectMethod]
    public virtual async Task HandleGetItemAction(GetItemAction<T> getItemAction, IDispatcher dispatcher)
    {
        var getItemResult = await _baseService.GetItemAsync(getItemAction.ItemId);

        if (getItemResult.IsSuccess && getItemResult.Value is not null)
            dispatcher.Dispatch(new GetItemResultAction<T>(getItemResult.Value));

        else
            dispatcher.Dispatch(new SetMessageAction(getItemResult.Message, MessageTypeEnum.Error));
    }

    [EffectMethod]
    public virtual async Task HandleCreateItemAction(CreateItemAction<T> itemCreationAction, IDispatcher dispatcher)
    {
        var itemCreationResult = await _baseService.CreateAsync(itemCreationAction.ItemToCreate, RabbitmqConstants.CreateRecipRoutingKey);

        var createdItem = itemCreationAction.ItemToCreate;
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
        var updateResult = await _baseService.UpdateAsync(updateItemAction.ItemToUpdate, RabbitmqConstants.UpdateRecipRoutingKey);

        var updatedItem = updateItemAction.ItemToUpdate;
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
        //var deleteItemResult = await _baseService.DeleteAsync(deleteItemAction.Id);

        //var messageType = MessageType.Error;
        //var deletedItemId = deleteItemAction.Id;

        //if (deleteItemResult.IsSuccess)
        //{
        //    messageType = MessageType.Success;
        //    deletedItemId = deleteItemResult.Value;
        //}

        //dispatcher.Dispatch(new DeleteItemResultAction<T>(deletedItemId, deleteItemResult.IsSuccess));
        //dispatcher.Dispatch(new SetMessageAction(deleteItemResult.Message, messageType));
    }
}
