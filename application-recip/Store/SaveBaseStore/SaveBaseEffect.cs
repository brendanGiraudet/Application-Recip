using application_recip.Enums;
using application_recip.Services.SaveBaseservice;
using application_recip.Store.BaseStore;
using application_recip.Store.MessageStore.Actions;
using application_recip.Store.SaveBaseStore.Actions;
using Fluxor;

namespace application_recip.Store.SaveBaseStore;

public class SaveBaseEffect<T>(ISaveBaseService<T> _baseService) 
    : GetBaseEffect<T>(_baseService)
    where T : class
{
    [EffectMethod]
    public virtual async Task HandleSaveItemsAction(SaveItemsAction<T> action, IDispatcher dispatcher)
    {
        var saveItemsResult = await _baseService.SaveItemsAsync(action.Items, action.ExchangeName, action.RoutingKey);

        var savedItems = action.Items;
        var messageType = MessageTypeEnum.Error;

        if (saveItemsResult.IsSuccess)
        {
            savedItems = saveItemsResult.Value;
            messageType = MessageTypeEnum.Success;
        }

        dispatcher.Dispatch(new SaveItemsResultAction<T>(savedItems, saveItemsResult.IsSuccess));
        dispatcher.Dispatch(new SetMessageAction(saveItemsResult.Message ?? string.Empty, messageType));
    }
}