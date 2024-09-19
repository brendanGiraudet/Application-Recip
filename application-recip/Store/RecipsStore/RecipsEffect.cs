using application_recip.Services.RecipsService;
using application_recip.Store.BaseStore;
using ms_recip.Ms_recip.Models;

namespace application_recip.Store.RecipsStore;

public class RecipsEffect(IRecipsService recipsService) : BaseEffect<RecipModel>(recipsService)
{
}
