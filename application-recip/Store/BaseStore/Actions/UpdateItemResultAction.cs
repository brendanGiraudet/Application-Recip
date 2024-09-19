namespace application_recip.Store.BaseStore.Actions;

public record UpdateItemResultAction<T>(T Item, bool isSuccess)
{
    public T Item { get; } = Item;
    
    public bool IsSuccess { get; } = isSuccess;
}
