using application_recip.Store.BaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.IngredientsStore;

[FeatureState]
public class IngredientsState : BaseState<IngredientModel>
{
    private IngredientsState()
    {
        
    }

    public IngredientsState(
        IngredientsState? currentState = null,
        ODataEnumerable<IngredientModel>? datagridItems = null,
        int? totalItems = null,
        IngredientModel? expectedItem = default
        )
        :base(currentState, datagridItems, totalItems, expectedItem) { }
}
