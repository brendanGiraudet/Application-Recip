using application_recip.Store.BaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.CategoriesStore;

[FeatureState]
public class CategoriesState : BaseState<CategoryModel>
{
    private CategoriesState()
    {
        
    }

    public CategoriesState(
        CategoriesState? currentState = null,
        ODataEnumerable<CategoryModel>? datagridItems = null,
        int? totalItems = null,
        CategoryModel? expectedItem = default
        )
        :base(currentState, datagridItems, totalItems, expectedItem) { }
}
