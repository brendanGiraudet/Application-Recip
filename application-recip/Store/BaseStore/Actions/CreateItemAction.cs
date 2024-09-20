namespace application_recip.Store.BaseStore.Actions;

public record CreateItemAction<T>(T Item, string ExchangeName, string RoutingKey)
{
}
