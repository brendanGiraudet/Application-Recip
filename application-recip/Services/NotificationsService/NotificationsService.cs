using application_recip.Constants;
using application_recip.Models;
using application_recip.Services.BaseService;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.UserInfoService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_notification.Default;
using ms_notification.Ms_notification.Models;

namespace application_recip.Services.NotificationsService;

public class NotificationsService : BaseService<NotificationModel>, INotificationsService
{
    private readonly MSNotificationSettings _msNotificationSettings;

    public NotificationsService(IHttpClientFactory httpClientFactory,
    IOptions<MSNotificationSettings> msNotificationSettingsOptions,
    IRabbitMqProducerService rabbitMqProducerService,
        IUserInfoService userInfoService)
        : base(
        nameof(Container.Notifications),
        nameof(NotificationModel.Id),
        httpClientFactory,
        msNotificationSettingsOptions.Value.OdataBaseUrl,
        rabbitMqProducerService,
        userInfoService,
        new Container(new(msNotificationSettingsOptions.Value.OdataBaseUrl)))
    {
        _msNotificationSettings = msNotificationSettingsOptions.Value;
    }
}
