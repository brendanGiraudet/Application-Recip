using application_recip.Store.BaseStore;
using Radzen;

namespace application_recip.Store.SaveBaseStore;

public class SaveBaseState<T> : GetBaseState<T>
{
    public IEnumerable<T> ItemsToSave {get; }

    private SaveBaseState()
    {
        ItemsToSave = [];
    }

    public SaveBaseState(
        SaveBaseState<T>? currentState = null,
        ODataEnumerable<T>? items = null,
        int? totalItems = null,
        T? expectedItem = default,
        IEnumerable<T>? itemsToSave = null
        )
        : base (currentState, items, totalItems, expectedItem)
    {
        ItemsToSave = itemsToSave ?? currentState?.ItemsToSave ?? [];
    }
}
