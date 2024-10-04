using System;

namespace application_recip.Store.SaveBaseStore.Actions;

public record AddItemToSaveAction<T>(T Item)
{

}
