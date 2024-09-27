namespace application_recip.Store.GetBaseStore.Actions;

public record GetItemAction<T>
{
    public Guid ItemId { get; }

    public GetItemAction(Guid itemId)
    {
        ItemId = itemId;
    }
}
