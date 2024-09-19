using application_recip.Constants;
using application_recip.Models;
using application_recip.Services.UserInfoService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_notification.Default;
using ms_notification.Ms_notification.Models;

namespace application_recip.Services.NotificationsService;

public class NotificationsService : INotificationsService
{
    private readonly MSNotificationSettings _msNotificationSettings;
    private readonly Container _odataContainer;
    private readonly IUserInfoService _userInfoService;

    public NotificationsService(
        IOptions<MSNotificationSettings> msConfigurationSettingsOptions,
        IUserInfoService userInfoService)
    {
        _msNotificationSettings = msConfigurationSettingsOptions.Value;
        _odataContainer = new Container(new Uri(_msNotificationSettings.OdataBaseUrl));
        _userInfoService = userInfoService;
    }

    public async Task<MethodResult<IEnumerable<NotificationModel>>> GetNotificationsAsync()
    {
        try
        {
            var notifications = _odataContainer.Notifications
                .Where(n => n.UserId == _userInfoService.GetUserId() 
                            && !n.Deleted
                            && n.ApplicationName == RabbitmqConstants.ApplicationName).ToList();

            return await Task.FromResult(MethodResult<IEnumerable<NotificationModel>>.CreateSuccessResult(notifications));
        }
        catch (Exception ex)
        {
            return MethodResult<IEnumerable<NotificationModel>>.CreateErrorResult(ex.Message);
        }
    }
}
