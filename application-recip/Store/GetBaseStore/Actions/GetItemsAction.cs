using Radzen;

namespace application_recip.Store.GetBaseStore.Actions;

public record GetItemsAction<T>
{
    public LoadDataArgs LoadDataArgs { get; }
    public string? Expand { get; }
    public string? Select { get; }
    public bool? Count { get; }

    public GetItemsAction(LoadDataArgs? loadDataArgs = null, string? expand = null, string? select = null, bool? count = null)
    {
        LoadDataArgs = loadDataArgs ?? new();
        Expand = expand;
        Select = select;
        Count = count;
    }
}
