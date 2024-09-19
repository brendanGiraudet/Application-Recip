using application_recip.Services.ConfigurationService;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.RecipsService;
using application_recip.Services.UserInfoService;
using application_recip.Settings;

namespace application_recip.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IConfigurationService, ConfigurationService>();
        services.AddTransient<IRabbitMqProducerService, RabbitMqProducerService>();
        services.AddTransient<IRecipsService, RecipsService>();
        
        services.AddSingleton<IUserInfoService, UserInfoService>();
    }

    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MSConfigurationSettings>(options => configuration.GetSection(key: "MSConfigurationSettings").Bind(options));
        
        services.Configure<MSRecipSettings>(options => configuration.GetSection(key: "MSRecipSettings").Bind(options));
    }
}
