using application_recip.Models;
using ms_configuration.Ms_configuration.Models;

namespace application_recip.Services.ConfigurationService;

public interface IConfigurationService
{
    Task<MethodResult<RabbitMqConfigModel>> GetRabbitMqConfigAsync();
}
