using System;

namespace application_recip.Store.SaveBaseStore.Actions;

public record RemoveItemToSaveAction<T>(T Item)
{

}
