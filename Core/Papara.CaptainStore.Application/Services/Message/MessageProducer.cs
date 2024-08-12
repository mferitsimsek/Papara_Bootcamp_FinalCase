using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using RabbitMQ.Client;
using System.Text;

namespace Papara.CaptainStore.Application.Services.Message
{
    public class MessageProducer : IMessageProducer
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _queueName = "notiQueue";

        public MessageProducer(IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQOptions.HostName,
                UserName = _rabbitMQOptions.UserName,
                Password = _rabbitMQOptions.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void ProduceMessage(NotificationTemplate template)
        {
            string message = JsonConvert.SerializeObject(template);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: properties, body: body);
        }

        // Dispose metodu ekleyin
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}