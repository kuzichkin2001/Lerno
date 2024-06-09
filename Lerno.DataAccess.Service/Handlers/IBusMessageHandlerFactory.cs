using Lerno.Shared.Enums;

namespace Lerno.DataAccess.Service.Handlers
{
    public interface IBusMessageHandlerFactory
    {
        IBusMessageHandler Create(BusMessageHandlerType handlerType);
    }
}
