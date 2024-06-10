using Lerno.Shared.Commands;

namespace Lerno.DataAccess.Service.Handlers
{
    public interface IBusMessageHandler
    {
        Task HandleEventAsync(string messageContent, CancellationToken cancellationToken = default);

        Task<object> HandleCommandAsync(string messageContent, CancellationToken cancellationToken = default);
    }
}
