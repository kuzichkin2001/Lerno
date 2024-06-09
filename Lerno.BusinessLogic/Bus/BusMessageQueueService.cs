using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Lerno.Configuration.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using RabbitMQ.Client.Events;

namespace Lerno.BusinessLogic.Bus
{
    public class BusMessageQueueService : IBusMessageQueueService
    {
        private readonly BusOptions _busOptions;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ConcurrentDictionary<string, TaskCompletionSource> _eventTasksMapper = new();
        private readonly ConcurrentDictionary<string, TaskCompletionSource<object>> _commandTasksMapper = new();

        private readonly string EVENTS_DEFAULT_QUEUE = "Lerno_RPC_events";
        private readonly string COMMANDS_DEFAULT_QUEUE = "Lerno_RPC_commands";

        public BusMessageQueueService(IOptions<BusOptions> busOptions)
        {
            _busOptions = busOptions.Value;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _busOptions.Host,
                UserName = _busOptions.UserName,
                Password = _busOptions.Password,
                Port = _busOptions.Port,
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: EVENTS_DEFAULT_QUEUE);
            _channel.QueueDeclare(queue: COMMANDS_DEFAULT_QUEUE);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (_, eventArgs) =>
            {
                if (eventArgs.RoutingKey.Equals(EVENTS_DEFAULT_QUEUE))
                {
                    if (_eventTasksMapper.TryRemove(eventArgs.BasicProperties.CorrelationId, out var etcs)) return;

                    if (etcs is not null)
                    {
                        etcs.TrySetResult();
                    }

                    return;
                }

                if (_commandTasksMapper.TryRemove(eventArgs.BasicProperties.CorrelationId, out var ctcs)) return;

                var body = eventArgs.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);

                if (ctcs is not null)
                {
                    ctcs.TrySetResult(response);
                }
            };
        }

        public async Task<TAnswer> SendCommandAsync<TBody, TAnswer>(BusMessage<TBody> busMessage,
            CancellationToken cancellationToken = default)
            where TBody : class
            where TAnswer : class
        {
            var message = ConstructMessage(busMessage);

            var result = await SendCommandAsync(message, cancellationToken).ConfigureAwait(false);
            var castedResult = (TAnswer)result;

            return castedResult;
        }

        public async Task SendEventAsync<TBody>(BusMessage<TBody> busMessage,
            CancellationToken cancellationToken = default)
            where TBody: class
        {
            var message = ConstructMessage(busMessage);

            await SendEventAsync(message, cancellationToken);
        }

        private string ConstructMessage<TBody>(BusMessage<TBody> busMessage)
            where TBody : class
        {
            var serializedMessage = JsonSerializer.Serialize(busMessage.Body);
            var messageType = busMessage.MessageType switch
            {
                BusMessageType.Object => "object",
                BusMessageType.Command => "command",
                BusMessageType.Event => "event"
            };
            var action = busMessage.Action;
            var handler = busMessage.Handler;
            var typeTag = nameof(TBody);

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

        private Task SendEventAsync(string message, CancellationToken cancellationToken)
        {
            var props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();

            props.CorrelationId = correlationId;
            props.ReplyTo = EVENTS_DEFAULT_QUEUE;

            var bytesMessage = Encoding.UTF8.GetBytes(message);
            var taskCompletionSource = new TaskCompletionSource();

            _eventTasksMapper.TryAdd(correlationId, taskCompletionSource);
            _channel.BasicPublish(exchange: string.Empty,
                routingKey: EVENTS_DEFAULT_QUEUE,
                basicProperties: props,
                body: bytesMessage);

            cancellationToken.Register(() => _eventTasksMapper.TryRemove(correlationId, out _));

            return taskCompletionSource.Task;
        }

        private Task<object> SendCommandAsync(string message, CancellationToken cancellationToken)
        {
            var props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();

            props.CorrelationId = correlationId;
            props.ReplyTo = COMMANDS_DEFAULT_QUEUE;

            var bytesMessage = Encoding.UTF8.GetBytes(message);
            var taskCompletionSource = new TaskCompletionSource<object>();

            _commandTasksMapper.TryAdd(correlationId, taskCompletionSource);
            _channel.BasicPublish(exchange: string.Empty,
                routingKey: COMMANDS_DEFAULT_QUEUE,
                basicProperties: props,
                body: bytesMessage);

            cancellationToken.Register(() => _commandTasksMapper.TryRemove(correlationId, out _));

            return taskCompletionSource.Task;
        }
    }
}
