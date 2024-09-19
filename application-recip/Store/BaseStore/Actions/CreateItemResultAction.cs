namespace application_recip.Store.BaseStore.Actions;

public record CreateItemResultAction<T>(T ItemToCreate, bool isSuccess)
{
    public T ItemToCreate { get; } = ItemToCreate;
    
    public bool IsSuccess { get; } = isSuccess;
}
