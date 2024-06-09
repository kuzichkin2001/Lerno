using Lerno.Shared.Enums;

namespace Lerno.Shared.Commands
{
    public class BusMessage<T> where T : class
    {
        public BusMessageType MessageType { get; set; }

        public string Action { get; set; }

        public BusMessageHandlerType Handler { get; set; }

        public T? Body { get; set; }
    }
}
