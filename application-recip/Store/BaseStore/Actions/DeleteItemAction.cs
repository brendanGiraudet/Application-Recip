namespace application_recip.Store.BaseStore.Actions;

public record DeleteItemAction<T>(T Item, string ExchangeName, string RoutingKey)
{
    
}
