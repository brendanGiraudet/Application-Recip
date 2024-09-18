using application_recip.Models;

namespace application_recip.Services.RabbitMqProducerService;

public interface IRabbitMqProducerService
{
    void PublishMessage<T>(RabbitMqMessageBase<T> message, string exchangeName, string routingKey);
}
