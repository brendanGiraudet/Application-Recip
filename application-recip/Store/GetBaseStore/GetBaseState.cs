using application_recip.Helpers;
using Radzen;

namespace application_recip.Store.BaseStore;

public class GetBaseState<T>
{
    public ODataEnumerable<T>? Items { get; }

    public int? TotalItems { get; }

    public T? ExpectedItem { get; }

    public T? ActualItem { get; }

    private GetBaseState()
    {
        Items = Enumerable.Empty<T>().AsODataEnumerable();

        TotalItems = 0;

        ExpectedItem = Activator.CreateInstance<T>();

        ActualItem = Activator.CreateInstance<T>();
    }

    public GetBaseState(GetBaseState<T>? currentState = null,
                        ODataEnumerable<T>? items = null,
                        int? totalItems = null,
                        T? expectedItem = default)
    {
        Items = items ?? currentState?.Items ?? Enumerable.Empty<T>().AsODataEnumerable();

        TotalItems = totalItems ?? currentState?.TotalItems ?? 0;

        ExpectedItem = expectedItem ?? (currentState != null ? currentState.ExpectedItem : Activator.CreateInstance<T>());

        ActualItem = CloneHelper<T>.Clone(ExpectedItem);
    }
}
