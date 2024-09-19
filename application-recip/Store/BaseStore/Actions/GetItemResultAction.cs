namespace application_recip.Store.BaseStore.Actions;

public record GetItemResultAction<T>
{
    public T Item { get; }

    public GetItemResultAction(T item)
    {
        Item = item;
    }
}