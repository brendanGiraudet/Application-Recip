using application_recip.Helpers;
using application_recip.Store.BaseStore;
using Radzen;

namespace application_recip.Store.SaveBaseStore;

public class SaveBaseState<T> : GetBaseState<T>
{
    public IEnumerable<T> ActualItemsToSave {get; }

    public IEnumerable<T> ExpectedItemsToSave {get; }

    private SaveBaseState()
    {
        ExpectedItemsToSave = [];
    }

    public SaveBaseState(
        SaveBaseState<T>? currentState = null,
        ODataEnumerable<T>? items = null,
        int? totalItems = null,
        T? expectedItem = default,
        IEnumerable<T>? itemsToSave = null,
        IEnumerable<T>? actualItemsToSave = null
        )
        : base (currentState, items, totalItems, expectedItem)
    {
        ExpectedItemsToSave = itemsToSave ?? currentState?.ExpectedItemsToSave ?? [];

        ActualItemsToSave = actualItemsToSave ?? currentState?.ActualItemsToSave ?? CloneHelper<T>.CloneEnumerable(ExpectedItemsToSave);
    }
}
