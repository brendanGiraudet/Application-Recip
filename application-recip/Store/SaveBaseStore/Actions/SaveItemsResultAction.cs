using System;

namespace application_recip.Store.SaveBaseStore.Actions;

public record SaveItemsResultAction<T> (IEnumerable<T>? Items, bool IsSuccess)
{
    
}
