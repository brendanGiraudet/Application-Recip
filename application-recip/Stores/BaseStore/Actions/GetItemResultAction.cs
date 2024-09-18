namespace application_recip.Stores.BaseStore.Actions;

public record GetItemResultAction<T>
{
    public T Item { get; }

    public GetItemResultAction(T item)
    {
        Item = item;
    }
}