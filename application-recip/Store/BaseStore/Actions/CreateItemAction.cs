namespace application_recip.Store.BaseStore.Actions;

public record CreateItemAction<T>(T ItemToCreate)
{
    public T ItemToCreate { get; } = ItemToCreate;
}
