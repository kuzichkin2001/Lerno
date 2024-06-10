using Lerno.Shared.Enums;

namespace Lerno.Shared.Commands
{
    public class BusMessage<TBody> where TBody : class
    {
        public BusMessageType MessageType { get; set; }

        public string Action { get; set; }

        public BusMessageHandlerType Handler { get; set; }

        public string TypeTag { get; set; }

        public TBody? Body { get; set; }
    }
}
