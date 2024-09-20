namespace application_recip.Store.BaseStore.Actions;

public record UpdateItemAction<T>(T Item, string ExchangeName, string RoutingKey)
{
}
