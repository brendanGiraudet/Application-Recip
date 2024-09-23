using application_recip.Services.BaseService;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.UserInfoService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_recip.Default;
using ms_recip.Ms_recip.Models;

namespace application_recip.Services.ProfilsService;

public class ProfilsService : BaseService<ProfilModel>, IProfilsService
{
    public ProfilsService(IHttpClientFactory httpClientFactory,
    IOptions<MSRecipSettings> msProfilSettingsOptions,
    IRabbitMqProducerService rabbitMqProducerService,
        IUserInfoService userInfoService)
        : base(
        nameof(Container.Profils),
        nameof(ProfilModel.Id),
        httpClientFactory,
        msProfilSettingsOptions.Value.OdataBaseUrl,
        rabbitMqProducerService,
        userInfoService,
        new Container(new(msProfilSettingsOptions.Value.OdataBaseUrl)))
    {
        _dataServiceQuery = new Container(new(msProfilSettingsOptions.Value.OdataBaseUrl)).Profils;
    }

    protected override string GetSuccessCreationItemMessages() => "Profil creation is in progress";
    protected override string GetFailedCreationItemMessages() => "Error when create profil";

    protected override string GetSuccessUpdateItemMessages() => "Profil update is in progress";
    protected override string GetFailedUpdateItemMessages() => "Error when update recip";

    protected override string GetSuccessDeleteItemMessages() => "Profil deletion is in progress";
    protected override string GetFailedDeleteItemMessages() => "Error when delete recip";
}
