using application_recip.Store.BaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.RecipsStore;

[FeatureState]
public class RecipsState : BaseState<RecipModel>
{
    private RecipsState()
    {
        
    }

    public RecipsState(
        RecipsState? currentState = null,
        ODataEnumerable<RecipModel>? datagridItems = null,
        int? totalItems = null,
        RecipModel? expectedItem = default
        )
        :base(currentState, datagridItems, totalItems, expectedItem) { }
}
