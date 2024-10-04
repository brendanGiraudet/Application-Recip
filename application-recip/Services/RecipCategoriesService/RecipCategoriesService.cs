using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.SaveBaseservice;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_recip.Default;
using ms_recip.Ms_recip.Models;

namespace application_recip.Services.RecipCategoriesService;

public class RecipCategoriesService : SaveBaseService<RecipCategoryModel>, IRecipCategoriesService
{
    private const string _entitySetName = "RecipCategories";
    public RecipCategoriesService(IHttpClientFactory httpClientFactory,
    IOptions<MSRecipSettings> msRecipSettingsOptions,
    IRabbitMqProducerService rabbitMqProducerService)
        : base(
        _entitySetName,
        nameof(RecipCategoryModel.RecipId),
        httpClientFactory.CreateClient(),
        msRecipSettingsOptions.Value.OdataBaseUrl,
        new Container(new(msRecipSettingsOptions.Value.OdataBaseUrl)),
        rabbitMqProducerService)
    {
        _dataServiceQuery = new Container(new(msRecipSettingsOptions.Value.OdataBaseUrl)).CreateQuery<RecipCategoryModel>(_entitySetName);
    }
}
