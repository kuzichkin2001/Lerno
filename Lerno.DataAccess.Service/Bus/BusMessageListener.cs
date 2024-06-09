
using Lerno.Configuration.Options;
using Lerno.DataAccess.Service.Handlers;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Lerno.DataAccess.Service.Bus
{
    public class BusMessageListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<BusMessageListener> _logger;
        private readonly IBusMessageHandlerFactory _busHandlerFactory;
        private readonly BusOptions _busOptions;

        private readonly string DEFAULT_QUEUE = "Lerno_RPC";

        public BusMessageListener(IOptions<BusOptions> busOptions,
            ILogger<BusMessageListener> logger,
            IBusMessageHandlerFactory busHandlerFactory)
        {
            _busOptions = busOptions.Value;
            _logger = logger;
            _busHandlerFactory = busHandlerFactory;
            
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

                var busMessageDict = ParseToDictionary(content);

                var busMessage = ParseBusMessage<object>(busMessageDict);

                _logger.LogInformation($"Получено сообщение: {content}");

                if (busMessage.MessageType == BusMessageType.Command)
                {
                    var busHandler = _busHandlerFactory.Create(busMessage.Handler);

                    var result = busHandler.Handle<object, object>(busMessage);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(DEFAULT_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        private Dictionary<string, string> ParseToDictionary(string messageContent)
        {
            var parameters = new Dictionary<string, string>();
            var parts = messageContent.Split('&');
            foreach (var part in parts)
            {
                var kvp = part.Split('=');
                var (key, value) = (kvp[0], kvp[1]);

                parameters[key] = value;
            }

            return parameters;
        }

        private BusMessage<TBody> ParseBusMessage<TBody>(Dictionary<string, string> parameters)
            where TBody : class
        {
            var busMessage = new BusMessage<TBody>();
            busMessage.Action = parameters["action"];
            busMessage.Handler = (BusMessageHandlerType)int.Parse(parameters["handler"]);
            busMessage.MessageType = (BusMessageType)int.Parse(parameters["message_type"]);

            var bodyType = Type.GetType(parameters["type_tag"]);
            var deserializedBody = JsonConvert.DeserializeObject(parameters["body"], bodyType);
            busMessage.Body = (TBody)deserializedBody;

            return busMessage;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();

            base.Dispose();
        }
    }
}
