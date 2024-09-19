using application_recip.Models;
using application_recip.Services.ConfigurationService;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace application_recip.Services.RabbitMqProducerService;

public class RabbitMqProducerService : IRabbitMqProducerService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqProducerService(IConfigurationService configurationService)
    {
        var rabbitMqConfigResult = configurationService.GetRabbitMqConfigAsync().Result;

        var rabbitMqConfig = rabbitMqConfigResult.Value;

        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig?.Hostname,
            Port = rabbitMqConfig?.Port ?? default,
            UserName = rabbitMqConfig?.Username,
            Password = rabbitMqConfig?.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void PublishMessage<T>(RabbitMqMessageBase<T> message, string exchangeName, string routingKey)
    {
        try
        {
            _channel.ExchangeDeclare(exchange: exchangeName, durable: true, type: ExchangeType.Topic);

            var jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            _channel.BasicPublish(
                exchange: exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );

            Console.WriteLine($"Message published to exchange {exchangeName} with routing key {routingKey}: {jsonMessage}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"send message proglem : {ex.Message}");
        }
    }

    void IDisposable.Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
