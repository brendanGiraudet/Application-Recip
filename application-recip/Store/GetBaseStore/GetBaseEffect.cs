using application_recip.Enums;
using application_recip.Services.GetBaseService;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.MessageStore.Actions;
using Fluxor;
using Radzen;

namespace application_recip.Store.BaseStore;

public class GetBaseEffect<T>(IGetBaseService<T> _baseService)
{
    [EffectMethod]
    public virtual async Task HandleGetItemsAction(GetDatagridItemsAction<T> getItemsWithFilterAction, IDispatcher dispatcher)
    {
        var getItemsResult = await _baseService.GetItemsAsync(args: getItemsWithFilterAction.LoadDataArgs, expand: getItemsWithFilterAction.Expand, select: getItemsWithFilterAction.Select, count: getItemsWithFilterAction.Count);

        if (getItemsResult.IsSuccess)
        {
            var items = getItemsResult.Value.Value.AsODataEnumerable();
            var top = getItemsWithFilterAction.LoadDataArgs.Top ?? 10;
            var count = getItemsResult.Value?.Count ?? 0;

            dispatcher.Dispatch(new GetDatagridItemsResultAction<T>(items, count, top));
        }
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

}