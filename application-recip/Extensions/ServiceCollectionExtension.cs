namespace application_recip.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        //services.AddTransient<INutrimentsService, NutrimentsService>();
    }

    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<BakaChiefAPI>(options => configuration.GetSection(key: "BakaChiefAPI").Bind(options));
    }
}
