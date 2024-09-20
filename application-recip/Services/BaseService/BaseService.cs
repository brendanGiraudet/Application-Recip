using application_recip.Constants;
using application_recip.Models;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.UserInfoService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OData.Client;
using ms_recip.Default;
using Radzen;
using System.Globalization;

namespace application_recip.Services.BaseService;

public class BaseService<T> : IBaseService<T> where T : class
{
    protected HttpClient _httpClient;

    protected string _entitySetName;

    protected string _propertyKeyName;

    protected Container _odataContainer;

    public required DataServiceQuery<T> _dataServiceQuery;

    protected IRabbitMqProducerService _rabbitMqProducerService;
    protected IUserInfoService _userInfoService;
    protected MSRecipSettings _mSRecipSettings;

    public BaseService(
        string entitySetName,
        string propertyKeyName,
        IHttpClientFactory httpClientFactory,
        IOptions<MSRecipSettings> msRecipSettingsOptions,
        IRabbitMqProducerService rabbitMqProducerService,
        IUserInfoService userInfoService)
    {
        _httpClient = httpClientFactory.CreateClient();

        _entitySetName = entitySetName;

        _propertyKeyName = propertyKeyName;

        _mSRecipSettings = msRecipSettingsOptions.Value;

        _odataContainer = new Container(new(_mSRecipSettings.OdataBaseUrl));

        _odataContainer.BuildingRequest += (sender, eventArgs) =>
        {
            Console.WriteLine(" " + eventArgs);
            // Log request
            Console.WriteLine($"Request: {eventArgs.RequestUri}");

            if (eventArgs.Descriptor != null)
            {
                Console.WriteLine("Desc " + eventArgs.Descriptor);
            }

            eventArgs.Headers.Add("Accept-Language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        };

        _rabbitMqProducerService = rabbitMqProducerService;

        _userInfoService = userInfoService;
    }

    /// <inheritdoc/>
    public virtual async Task<ODataServiceResult<T>> GetDatagridItemsAsync(LoadDataArgs args, string? expand = null, string? select = null, bool? count = null)
    {
        try
        {
            var url = $"{_mSRecipSettings.OdataBaseUrl}/{_entitySetName}";
            var uri = new Uri(url);
            uri = uri.GetODataUri(filter: args.Filter, top: args.Top, skip: args.Skip, orderby: args.OrderBy, expand: expand, select: select, count: count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<T>>();
        }
        catch (System.Exception ex)
        {
            return new();
        }
    }

    /// <inheritdoc/>
    public virtual async Task<MethodResult<T>> GetItemAsync(Guid id)
    {
        try
        {
            var _keys = new Dictionary<string, object>
            {
                { _propertyKeyName, id }
            };

            var querySingle = new DataServiceQuerySingle<T>(_odataContainer, _dataServiceQuery.GetKeyPath(Serializer.GetKeyString(_odataContainer, _keys)));

            var selectedItem = await querySingle.GetValueAsync();

            return MethodResult<T>.CreateSuccessResult(selectedItem);
        }
        catch (System.Exception ex)
        {
            return MethodResult<T>.CreateErrorResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<T>> CreateAsync(T itemToCreate, string routingKey)
    {
        return await SendRabbitMqMessageAsync(itemToCreate, routingKey, GetSuccessCreationItemMessages(), GetFailedCreationItemMessages());
    }

    protected virtual string GetSuccessCreationItemMessages() => "Create_Success";
    protected virtual string GetFailedCreationItemMessages() => "Create_Error";

    /// <inheritdoc/>
    public async Task<MethodResult<T>> UpdateAsync(T itemToUpdate, string routingKey)
    {
        return await SendRabbitMqMessageAsync(itemToUpdate, routingKey, GetSuccessUpdateItemMessages(), GetFailedUpdateItemMessages());
    }

    protected virtual string GetSuccessUpdateItemMessages() => "Update_Success";
    protected virtual string GetFailedUpdateItemMessages() => "Update_Error";

    /// <inheritdoc/>
    public async Task<MethodResult<T>> DeleteAsync(T itemToDelete, string routingKey)
    {
        return await SendRabbitMqMessageAsync(itemToDelete, routingKey, GetSuccessDeleteItemMessages(), GetFailedDeleteItemMessages());
    }

    protected virtual string GetSuccessDeleteItemMessages() => "Delete_Success";
    protected virtual string GetFailedDeleteItemMessages() => "Delete_Error";

    public async Task<MethodResult<T>> SendRabbitMqMessageAsync(T item, string routingKey, string successMessage, string failedMessage)
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
            _rabbitMqProducerService.PublishMessage(message, RabbitmqConstants.RecipExchangeName, routingKey);

            return MethodResult<T>.CreateSuccessResult(item, successMessage);
        }
        catch (System.Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);

            return MethodResult<T>.CreateErrorResult(failedMessage);
        }
    }
}
