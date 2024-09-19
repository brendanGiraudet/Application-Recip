namespace application_recip.Store.BaseStore.Actions;

public record GetItemAction<T>
{
    public Guid ItemId { get; }

    public GetItemAction(Guid itemId)
    {
        ItemId = itemId;
    }
}
