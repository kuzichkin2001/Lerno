using Lerno.Shared.Commands;

namespace Lerno.BusinessLogic.Bus
{
    public interface IBusMessageQueueService
    {
        Task<TAnswer> SendCommandAsync<TBody, TAnswer>(BusMessage<TBody> busMessage,
            CancellationToken cancellationToken = default)
            where TBody : class
            where TAnswer : class;

        Task SendEventAsync<TBody>(BusMessage<TBody> busMessage,
            CancellationToken cancellationToken = default)
            where TBody : class;
    }
}
