﻿using application_recip.Services.BaseService;
using application_recip.Services.RabbitMqProducerService;
using application_recip.Services.UserInfoService;
using application_recip.Settings;
using Microsoft.Extensions.Options;
using ms_recip.Default;
using ms_recip.Ms_recip.Models;

namespace application_recip.Services.IngredientsService;

public class IngredientsService : BaseService<IngredientModel>, IIngredientsService
{
    public IngredientsService(IHttpClientFactory httpClientFactory,
    IOptions<MSRecipSettings> msRecipSettingsOptions,
    IRabbitMqProducerService rabbitMqProducerService,
        IUserInfoService userInfoService)
        : base(
        nameof(Container.Ingredients),
        nameof(IngredientModel.Id),
        httpClientFactory,
        msRecipSettingsOptions.Value.OdataBaseUrl,
        rabbitMqProducerService,
        userInfoService,
        new Container(new(msRecipSettingsOptions.Value.OdataBaseUrl)))
    {
        _dataServiceQuery = new Container(new(msRecipSettingsOptions.Value.OdataBaseUrl)).Ingredients;
    }

    protected override string GetSuccessCreationItemMessages() => "Ingredient creation is in progress";
    protected override string GetFailedCreationItemMessages() => "Error when create recip";

    protected override string GetSuccessUpdateItemMessages() => "Ingredient update is in progress";
    protected override string GetFailedUpdateItemMessages() => "Error when update recip";

    protected override string GetSuccessDeleteItemMessages() => "Ingredient deletion is in progress";
    protected override string GetFailedDeleteItemMessages() => "Error when delete recip";
}
