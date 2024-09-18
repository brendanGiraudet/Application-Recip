namespace application_recip.Stores.BaseStore.Actions;

public record CreateItemAction<T>(T ItemToCreate)
{
    public T ItemToCreate { get; } = ItemToCreate;
}
