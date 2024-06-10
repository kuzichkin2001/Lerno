using Lerno.Configuration.Options;
using Lerno.DataAccess.Service.Handlers;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Lerno.DataAccess.Service.Bus
{
    public class BusMessageListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<BusMessageListener> _logger;
        private readonly IBusMessageHandlerFactory _busHandlerFactory;
        private readonly BusOptions _busOptions;

        private readonly string EVENTS_DEFAULT_QUEUE = "Lerno_RPC_events";
        private readonly string COMMANDS_DEFAULT_QUEUE = "Lerno_RPC_commands";

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

            _channel.QueueDeclare(queue: EVENTS_DEFAULT_QUEUE, exclusive: false);
            _channel.QueueDeclare(queue: COMMANDS_DEFAULT_QUEUE, exclusive: false);
        }

        protected override Task ExecuteAsync(CancellationToken cToken)
        {
            cToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (_, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var busMessageDict = ParseToDictionary(content);

                var messageType = ExtractMessageType(busMessageDict);
                var handler = ExtractHandler(busMessageDict);

                _logger.LogInformation($"Получено сообщение: {content}");

                var busHandler = _busHandlerFactory.Create(handler);
                
                if (messageType == BusMessageType.Command)
                {
                    var result = await busHandler.HandleCommandAsync(content);

                    var props = _channel.CreateBasicProperties();
                    props.ReplyTo = COMMANDS_DEFAULT_QUEUE;
                    props.CorrelationId = ea.BasicProperties.CorrelationId;

                    var serializedReply = JsonConvert.SerializeObject(result);
                    var replyMessageBytes = Encoding.UTF8.GetBytes(serializedReply);

                    _channel.BasicPublish(exchange: "",
                        routingKey: props.ReplyTo,
                        basicProperties: props,
                        body: replyMessageBytes);
                }
                else
                {
                    await busHandler.HandleEventAsync(content);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(EVENTS_DEFAULT_QUEUE, false, consumer);
            _channel.BasicConsume(COMMANDS_DEFAULT_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        private string ConstructMessage<TBody>(BusMessage<TBody> busMessage)
            where TBody : class
        {
            var serializedMessage = JsonConvert.SerializeObject(busMessage.Body);
            var messageType = busMessage.MessageType.ToString("D");
            var action = busMessage.Action;
            var handler = busMessage.Handler.ToString("D");
            var typeTag = typeof(TBody).FullName;

            var messageBuilder = new StringBuilder();
            var message = messageBuilder
                .Append($"message_type={messageType}&")
                .Append($"action={action}&")
                .Append($"handler={handler}&")
                .Append($"type_tag={typeTag}&")
                .Append($"body={serializedMessage}")
                .ToString();

            return message;
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

        private BusMessageType ExtractMessageType(Dictionary<string, string> parameters)
        {
            return (BusMessageType)int.Parse(parameters["message_type"]);
        }

        private BusMessageHandlerType ExtractHandler(Dictionary<string, string> parameters)
        {
            return (BusMessageHandlerType)int.Parse(parameters["handler"]);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();

            base.Dispose();
        }
    }
}
