using application_recip.Stores.BaseStore;
using Fluxor;
using ms_recip.Ms_recip.Models;

namespace application_recip.Stores.RecipsStore;

[FeatureState]
public record RecipsState : BaseState<RecipModel>
{

}
