using application_recip.Store.BaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Store.ProfilsStore;

[FeatureState]
public class ProfilsState : BaseState<ProfilModel>
{
    private ProfilsState()
    {
        
    }

    public ProfilsState(
        ProfilsState? currentState = null,
        ODataEnumerable<ProfilModel>? datagridItems = null,
        int? totalItems = null,
        ProfilModel? expectedItem = default
        )
        :base(currentState, datagridItems, totalItems, expectedItem) { }
}
