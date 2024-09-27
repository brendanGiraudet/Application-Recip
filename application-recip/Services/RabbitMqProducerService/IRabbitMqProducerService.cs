using application_recip.Models;

namespace application_recip.Services.RabbitMqProducerService;

public interface IRabbitMqProducerService
{
    void PublishMessage<T>(RabbitMqMessageBase<T> message, string exchangeName, string routingKey);

    Task<MethodResult<T>> SendRabbitMqMessageAsync<T>(T item, string exchangeName, string routingKey, string successMessage, string failedMessage);
}
