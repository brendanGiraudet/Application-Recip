namespace application_recip.Stores.BaseStore.Actions;

public record GetItemAction<T>
{
    public Guid ItemId { get; }

    public GetItemAction(Guid itemId)
    {
        ItemId = itemId;
    }
}
