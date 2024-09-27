using application_recip.Store.BaseStore;
using Radzen;

namespace application_recip.Store.BaseStore;

public class BaseState<T> : GetBaseState<T>
{
    private BaseState()
    {
        
    }

    public BaseState(
        BaseState<T>? currentState = null,
        ODataEnumerable<T>? items = null,
        int? totalItems = null,
        T? expectedItem = default
        )
        : base (currentState, items, totalItems, expectedItem)
    {
        
    }
}
