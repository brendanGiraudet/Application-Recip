using application_recip.Constants;
using application_recip.Models;
using application_recip.Services.ConfigurationService;
using application_recip.Services.UserInfoService;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace application_recip.Services.RabbitMqProducerService;

public class RabbitMqProducerService : IRabbitMqProducerService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    protected IUserInfoService _userInfoService;

    public RabbitMqProducerService(IConfigurationService configurationService,
                                   IUserInfoService userInfoService)
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
        _userInfoService = userInfoService;
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

    public async Task<MethodResult<T>> SendRabbitMqMessageAsync<T>(T item, string exchangeName, string routingKey, string successMessage, string failedMessage)
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
            
            PublishMessage(message, exchangeName, routingKey);

            return MethodResult<T>.CreateSuccessResult(item, successMessage);
        }
        catch (System.Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);

            return MethodResult<T>.CreateErrorResult(failedMessage);
        }
    }

    void IDisposable.Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
