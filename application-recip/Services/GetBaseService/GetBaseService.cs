using System.Globalization;
using application_recip.Models;
using Microsoft.OData.Client;
using Radzen;

namespace application_recip.Services.GetBaseService;

public class GetBaseService<T> : IGetBaseService<T>
{
    protected HttpClient _httpClient;

    protected string _entitySetName;

    protected string _propertyKeyName;

    protected DataServiceContext _odataContainer;

    protected string _odataUrl;

    public required DataServiceQuery<T> _dataServiceQuery;

    public GetBaseService(HttpClient httpClient,
                          string entitySetName,
                          string propertyKeyName,
                          DataServiceContext odataContainer,
                          string odataUrl)

    {
        _httpClient = httpClient;
        _entitySetName = entitySetName;
        _propertyKeyName = propertyKeyName;
        _odataContainer = odataContainer;
        _odataUrl = odataUrl;

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
    }

    /// <inheritdoc/>
    public virtual async Task<MethodResult<ODataServiceResult<T>>> GetItemsAsync(LoadDataArgs args, string? expand = null, string? select = null, bool? count = null)
    {
        try
        {
            var url = $"{_odataUrl}/{_entitySetName}";
            var uri = new Uri(url);
            uri = uri.GetODataUri(filter: args.Filter, top: args.Top, skip: args.Skip, orderby: args.OrderBy, expand: expand, select: select, count: count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            var values = await response.ReadAsync<ODataServiceResult<T>>();

            return MethodResult<ODataServiceResult<T>>.CreateSuccessResult(values);
        }
        catch (System.Exception ex)
        {
            return MethodResult<ODataServiceResult<T>>.CreateErrorResult(ex.Message);
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
}
