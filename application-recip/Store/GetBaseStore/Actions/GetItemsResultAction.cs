using Radzen;

namespace application_recip.Store.GetBaseStore.Actions;

public record GetItemsResultAction<T>
{
    public ODataEnumerable<T> Items { get; }

    public int Count { get; }

    public int Take { get; }

    public GetItemsResultAction(ODataEnumerable<T> items, int count, int take)
    {
        Items = items;
        Count = count;
        Take = take;
    }
}