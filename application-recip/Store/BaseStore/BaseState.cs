using Radzen;

namespace application_recip.Store.BaseStore;

public class BaseState<T>
{
    public ODataEnumerable<T>? DatagridItems { get; }

    public int? TotalItems { get; }

    public T? ExpectedItem { get; }

    private BaseState() 
    {
        DatagridItems = Enumerable.Empty<T>().AsODataEnumerable();
        
        TotalItems = 0;
        
        ExpectedItem = Activator.CreateInstance<T>();
    }
    
    public BaseState(
        BaseState<T>? currentState = null,
        ODataEnumerable<T>? datagridItems = null,
        int? totalItems = null,
        T? expectedItem = default
        ) 
    {
        DatagridItems = datagridItems ?? currentState?.DatagridItems ?? Enumerable.Empty<T>().AsODataEnumerable();
        
        TotalItems = totalItems ?? currentState?.TotalItems ?? 0;
        
        ExpectedItem = expectedItem ?? (currentState != null ? currentState.ExpectedItem : Activator.CreateInstance<T>());
    }
}
