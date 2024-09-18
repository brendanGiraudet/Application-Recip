using application_recip.Services.BaseService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_recip.Default;
using ms_recip.Ms_recip.Models;

namespace application_recip.Services.RecipsService;

public class RecipsService(IHttpClientFactory httpClientFactory, 
    IOptions<MSRecipSettings> msRecipSettingsOptions) 
    
    : BaseService<RecipModel>(
        nameof(Container.Recips),
        nameof(RecipModel.Id),
        httpClientFactory,
        msRecipSettingsOptions), 
    IRecipsService
{
}
