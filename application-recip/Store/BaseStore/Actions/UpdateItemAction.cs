namespace application_recip.Store.BaseStore.Actions;

public record UpdateItemAction<T>(T ItemToUpdate)
{
    public T ItemToUpdate { get; } = ItemToUpdate;
}
