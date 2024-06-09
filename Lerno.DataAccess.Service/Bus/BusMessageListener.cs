
using Lerno.Configuration.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace Lerno.DataAccess.Service.Bus
{
    public class BusMessageListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<BusMessageListener> _logger;
        private readonly BusOptions _busOptions;

        private readonly string DEFAULT_QUEUE = "Lerno_RPC";

        public BusMessageListener(IOptions<BusOptions> busOptions,
            ILogger<BusMessageListener> logger)
        {
            _busOptions = busOptions.Value;
            _logger = logger;
            
            var connectionFactory = new ConnectionFactory() {
                HostName = _busOptions.Host,
                UserName = _busOptions.UserName,
                Password = _busOptions.Password,
                Port = _busOptions.Port,
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: DEFAULT_QUEUE);
        }

        protected override Task ExecuteAsync(CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (_, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                _logger.LogInformation($"Получено сообщение: {content}");

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(DEFAULT_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();

            base.Dispose();
        }
    }
}
