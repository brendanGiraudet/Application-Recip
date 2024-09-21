using application_recip.HostedServices.NotificationsHostedService;
using application_recip.Services.CategoriesService;
using application_recip.Services.ConfigurationService;
using application_recip.Services.IngredientsService;
using application_recip.Services.NotificationsService;
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
        services.AddTransient<INotificationsService, NotificationsService>();
        services.AddTransient<IIngredientsService, IngredientsService>();
        services.AddTransient<ICategoriesService, CategoriesService>();

        services.AddScoped<NotificationsHostedService>();

        services.AddSingleton<IUserInfoService, UserInfoService>();
    }

    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MSConfigurationSettings>(options => configuration.GetSection(key: "MSConfigurationSettings").Bind(options));

        services.Configure<MSRecipSettings>(options => configuration.GetSection(key: "MSRecipSettings").Bind(options));

        services.Configure<MSNotificationSettings>(options => configuration.GetSection(key: "MSNotificationSettings").Bind(options));
    }

    public static void AddCustomHostedServices(this IServiceCollection services)
    {
        //services.AddHostedService<NotificationsHostedservice>();
    }
}
