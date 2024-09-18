using application_recip.Services.RecipsService;
using application_recip.Stores.BaseStore;
using ms_recip.Ms_recip.Models;

namespace application_recip.Stores.RecipsStore;

public class RecipsEffect(IRecipsService recipsService) : BaseEffect<RecipModel>(recipsService)
{
}
