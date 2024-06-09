using Lerno.Shared.Commands;

namespace Lerno.DataAccess.Service.Handlers
{
    public interface IBusMessageHandler
    {
        TAnswer Handle<TBody, TAnswer>(BusMessage<TBody> busMessage)
            where TBody : class
            where TAnswer : class;
    }
}
