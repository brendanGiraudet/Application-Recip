using application_recip.Services.BaseService;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.UserInfoService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_recip.Default;
using ms_recip.Ms_recip.Models;

namespace application_recip.Services.RecipsService;

public class RecipsService(IHttpClientFactory httpClientFactory, 
    IOptions<MSRecipSettings> msRecipSettingsOptions,
    IRabbitMqProducerService rabbitMqProducerService,
        IUserInfoService userInfoService) 
    
    : BaseService<RecipModel>(
        nameof(Container.Recips),
        nameof(RecipModel.Id),
        httpClientFactory,
        msRecipSettingsOptions,
        rabbitMqProducerService,
        userInfoService), 
    IRecipsService
{

    protected override string GetSuccessCreationItemMessages() => "Recip creation is in progress";
}
