using System;

namespace application_recip.Store.SaveBaseStore.Actions;

public record SaveItemsAction<T>(IEnumerable<T> Items, string ExchangeName, string RoutingKey)
{
    
}
