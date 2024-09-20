namespace application_recip.Store.BaseStore.Actions;

public record UpdateItemResultAction<T>(T Item, bool IsSuccess)
{
    public T Item { get; } = Item;
    
    public bool IsSuccess { get; } = IsSuccess;
}
