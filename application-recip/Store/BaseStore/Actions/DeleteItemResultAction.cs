namespace application_recip.Store.BaseStore.Actions;

public record DeleteItemResultAction<T>(T Item, bool IsSuccess) { }
