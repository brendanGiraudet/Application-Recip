namespace application_recip.Store.BaseStore.Actions;

public record DeleteItemAction<T>
{
    public T Item { get; }

    public DeleteItemAction(T item)
    {
        Item = item;
    }
}
