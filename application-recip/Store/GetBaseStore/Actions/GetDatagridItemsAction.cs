using Radzen;

namespace application_recip.Store.GetBaseStore.Actions;

public record GetDatagridItemsAction<T>
{
    public LoadDataArgs LoadDataArgs { get; }
    public string? Expand { get; }
    public string? Select { get; }
    public bool? Count { get; }

    public GetDatagridItemsAction(LoadDataArgs loadDataArgs, string? expand = null, string? select = null, bool? count = null)
    {
        LoadDataArgs = loadDataArgs;
        Expand = expand;
        Select = select;
        Count = count;
    }
}
