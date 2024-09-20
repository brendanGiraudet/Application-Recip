using Radzen;

namespace application_recip.Store.BaseStore;

public class BaseState<T>
{
    public ODataEnumerable<T>? Items { get; }

    public int? TotalItems { get; }

    public T? ExpectedItem { get; }

    private BaseState() 
    {
        Items = Enumerable.Empty<T>().AsODataEnumerable();
        
        TotalItems = 0;
        
        ExpectedItem = Activator.CreateInstance<T>();
    }
    
    public BaseState(
        BaseState<T>? currentState = null,
        ODataEnumerable<T>? items = null,
        int? totalItems = null,
        T? expectedItem = default
        ) 
    {
        Items = items ?? currentState?.Items ?? Enumerable.Empty<T>().AsODataEnumerable();
        
        TotalItems = totalItems ?? currentState?.TotalItems ?? 0;
        
        ExpectedItem = expectedItem ?? (currentState != null ? currentState.ExpectedItem : Activator.CreateInstance<T>());
    }
}
