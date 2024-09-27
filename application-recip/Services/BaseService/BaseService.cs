using application_recip.Constants;
using application_recip.Models;
using application_recip.Services.GetBaseService;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.UserInfoService;
using Microsoft.OData.Client;

namespace application_recip.Services.BaseService;

public class BaseService<T> : GetBaseService<T>, IBaseService<T> where T : class
{
    protected IRabbitMqProducerService _rabbitMqProducerService;
    protected IUserInfoService _userInfoService;

    public BaseService(
        string entitySetName,
        string propertyKeyName,
        IHttpClientFactory httpClientFactory,
        string odataUrl,
        IRabbitMqProducerService rabbitMqProducerService,
        IUserInfoService userInfoService,
        DataServiceContext odataContainer)
        : base (httpClientFactory.CreateClient(),
                entitySetName,
                propertyKeyName,
                odataContainer,
                odataUrl)
    {
        _rabbitMqProducerService = rabbitMqProducerService;

        _userInfoService = userInfoService;
    }

    /// <inheritdoc/>
    public async Task<MethodResult<T>> CreateAsync(T itemToCreate, string exchangeName, string routingKey)
    {
        return await SendRabbitMqMessageAsync(itemToCreate, exchangeName, routingKey, GetSuccessCreationItemMessages(), GetFailedCreationItemMessages());
    }

    protected virtual string GetSuccessCreationItemMessages() => "Create_Success";
    protected virtual string GetFailedCreationItemMessages() => "Create_Error";

    /// <inheritdoc/>
    public async Task<MethodResult<T>> UpdateAsync(T itemToUpdate, string exchangeName, string routingKey)
    {
        return await SendRabbitMqMessageAsync(itemToUpdate, exchangeName, routingKey, GetSuccessUpdateItemMessages(), GetFailedUpdateItemMessages());
    }

    protected virtual string GetSuccessUpdateItemMessages() => "Update_Success";
    protected virtual string GetFailedUpdateItemMessages() => "Update_Error";

    /// <inheritdoc/>
    public async Task<MethodResult<T>> DeleteAsync(T itemToDelete, string exchangeName, string routingKey)
    {
        return await SendRabbitMqMessageAsync(itemToDelete, exchangeName, routingKey, GetSuccessDeleteItemMessages(), GetFailedDeleteItemMessages());
    }

    protected virtual string GetSuccessDeleteItemMessages() => "Delete_Success";
    protected virtual string GetFailedDeleteItemMessages() => "Delete_Error";

    public async Task<MethodResult<T>> SendRabbitMqMessageAsync(T item, string exchangeName, string routingKey, string successMessage, string failedMessage)
    {
        try
        {
            var message = new RabbitMqMessageBase<T>
            {
                ApplicationName = RabbitmqConstants.ApplicationName,
                RoutingKey = routingKey,
                Timestamp = DateTime.UtcNow,
                UserId = _userInfoService.GetUserId(),
                Payload = item
            };
            _rabbitMqProducerService.PublishMessage(message, exchangeName, routingKey);

            return MethodResult<T>.CreateSuccessResult(item, successMessage);
        }
        catch (System.Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);

            return MethodResult<T>.CreateErrorResult(failedMessage);
        }
    }
}
