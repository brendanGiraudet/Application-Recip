using Radzen;

namespace application_recip.Stores.BaseStore.Actions;

public record GetDatagridItemsResultAction<T>
{
    public ODataEnumerable<T> Items { get; }

    public int Count { get; }

    public int Take { get; }

    public GetDatagridItemsResultAction(ODataEnumerable<T> items, int count, int take)
    {
        Items = items;
        Count = count;
        Take = take;
    }
}