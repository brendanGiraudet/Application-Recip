﻿using application_recip.Models;
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

    protected DataServiceQuery<T> _dataServiceQuery;

    public BaseService(
        string entitySetName,
        string propertyKeyName,
        IHttpClientFactory httpClientFactory,
        IOptions<MSRecipSettings> msRecipSettingsOptions)
    {
        _httpClient = httpClientFactory.CreateClient();

        _entitySetName = entitySetName;

        _propertyKeyName = propertyKeyName;

        _odataContainer = new Container(new(msRecipSettingsOptions.Value.OdataBaseUrl));

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
    public virtual async Task<ODataServiceResult<T>> GetDatagridItemsAsync(LoadDataArgs args, string expand = default(string), string select = default(string), bool? count = default(bool?))
    {
        try
        {
            var uri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}/{_entitySetName}");
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

            var selectedItem = querySingle.GetValue();

            return MethodResult<T>.CreateSuccessResult(selectedItem);
        }
        catch (System.Exception ex)
        {
            return MethodResult<T>.CreateErrorResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<T>> CreateAsync(T itemToCreate)
    {
        try
        {
            _odataContainer.Detach(itemToCreate);
            _odataContainer.AddObject(_entitySetName, itemToCreate);

            var response = await _odataContainer.SaveChangesAsync();

            if (response.First().StatusCode != StatusCodes.Status201Created)
            {
                return MethodResult<T>.CreateErrorResult("Create_Problem");
            }

            _odataContainer.Detach(itemToCreate);

            return MethodResult<T>.CreateSuccessResult(itemToCreate, "Create_Success");
        }
        catch (System.Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);

            return MethodResult<T>.CreateErrorResult("Create_Problem");
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<T>> UpdateAsync(decimal id, T itemToUpdate)
    {
        try
        {
            var uri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}/{_entitySetName}/{id}");

            var response = await _httpClient.PatchAsJsonAsync(uri, itemToUpdate);

            if (!response.IsSuccessStatusCode)
                return MethodResult<T>.CreateErrorResult("Update_Problem");


            return MethodResult<T>.CreateSuccessResult(itemToUpdate, "Update_Success");
        }
        catch (System.Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);

            return MethodResult<T>.CreateErrorResult("Update_Problem");
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<decimal>> DeleteAsync(decimal id)
    {
        try
        {
            var _keys = new Dictionary<string, object>
            {
                { _propertyKeyName, id }
            };

            var querySingle = new DataServiceQuerySingle<T>(_odataContainer, _dataServiceQuery.GetKeyPath(Serializer.GetKeyString(_odataContainer, _keys)));

            var itemToDelete = querySingle.GetValue();

            _odataContainer.Detach(itemToDelete);
            _odataContainer.AttachTo(_entitySetName, itemToDelete);
            _odataContainer.DeleteObject(itemToDelete);

            var response = await _odataContainer.SaveChangesAsync();

            if (response.First().StatusCode != StatusCodes.Status200OK)
            {
                return MethodResult<decimal>.CreateErrorResult("Delete_Problem");
            }

            return MethodResult<decimal>.CreateSuccessResult(id, "Delete_Success");
        }
        catch (System.Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);

            return MethodResult<decimal>.CreateErrorResult("Delete_Problem");
        }
    }
}
