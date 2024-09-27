using application_recip.Models;
using application_recip.Services.GetBaseService;
using application_recip.Services.RabbitMqProducerService;
using Microsoft.OData.Client;

namespace application_recip.Services.SaveBaseservice;

public class SaveBaseService<T> : GetBaseService<T>, ISaveBaseService<T>
{
    protected IRabbitMqProducerService _rabbitMqProducerService;

    public SaveBaseService(
        string entitySetName,
        string propertyKeyName,
        IHttpClientFactory httpClientFactory,
        string odataUrl,
        DataServiceContext odataContainer,
        IRabbitMqProducerService rabbitMqProducerService)
        : base(httpClientFactory.CreateClient(),
                entitySetName,
                propertyKeyName,
                odataContainer,
                odataUrl)
    {
        _rabbitMqProducerService = rabbitMqProducerService;
    }

    public async Task<MethodResult<IEnumerable<T>>> SaveItemsAsync(IEnumerable<T> items, string exchangeName, string routingKey)
    {
        return await _rabbitMqProducerService.SendRabbitMqMessageAsync(items, exchangeName, routingKey, GetSuccessCreationItemsMessages(), GetFailedCreationItemsMessages());
    }

    protected virtual string GetSuccessCreationItemsMessages() => "Create_Success";
    protected virtual string GetFailedCreationItemsMessages() => "Create_Error";
}
