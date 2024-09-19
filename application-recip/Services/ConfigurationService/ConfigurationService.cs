using Microsoft.Extensions.Options;
using application_recip.Models;
using application_recip.Settings;
using ms_configuration.Default;
using ms_configuration.Ms_configuration.Models;

namespace application_recip.Services.ConfigurationService;

public class ConfigurationService : IConfigurationService
{
    private readonly MSConfigurationSettings _msConfigurationSettings;
    private readonly Container _odataContainer;

    public ConfigurationService(
        IOptions<MSConfigurationSettings> msConfigurationSettingsOptions)
    {
        _msConfigurationSettings = msConfigurationSettingsOptions.Value;
        _odataContainer = new Container(new Uri(_msConfigurationSettings.OdataBaseUrl));
    }

    public async Task<MethodResult<RabbitMqConfigModel>> GetRabbitMqConfigAsync()
    {
        try
        {
            var rabbitMqConfig = _odataContainer.RabbitMqConfigs.OrderByDescending(c => c.CreationDate).FirstOrDefault();

            return await Task.FromResult(MethodResult<RabbitMqConfigModel>.CreateSuccessResult(rabbitMqConfig));
        }
        catch (Exception ex)
        {
            return MethodResult<RabbitMqConfigModel>.CreateErrorResult(ex.Message);
        }
    }
}
