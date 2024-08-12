using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Interfaces.Notification;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System.Text;

namespace Papara.CaptainStore.Application.Services.Message
{
    public class MessageConsumer : IMessageConsumer, IDisposable
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _queueName = "notiQueue";
        private bool _isConsuming = false; // Tüketici çalışıp çalışmadığı kontrol değişkeni.
        public MessageConsumer(IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            try
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
            catch (Exception ex)
            {
                Log.Warning($"RabbitMQ'ya bağlanılamadı: {ex.Message}");
            }
        }

        public void StartConsuming(INotificationService notificationService)
        {
            if (_isConsuming) return; // Zaten tüketiliyorsa, hiçbir işlem yapma
            if (_channel == null)
            {
                Log.Warning("RabbitMQ bağlantısı kurulamadı. BasicConsume işlemi yapılamaz.");
                return;
            }

            _isConsuming = true; // Tüketici başlatıldı
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var item = JsonConvert.DeserializeObject<NotificationTemplate>(message);

                try
                {
                    await notificationService.SendEmailAsync(item);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "E-posta gönderme işlemi sırasında hata oluştu. Mesaj: {Message}", message);
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }
        public void StopConsuming()
        {
            if (!_isConsuming) return;  // Tüketici çalışmıyorsa, hiçbir işlem yapma

            _channel.Close();
            _connection.Close();
            _isConsuming = false; // Tüketici durduruldu
        }
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}