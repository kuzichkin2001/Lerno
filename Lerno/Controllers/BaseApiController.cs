using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Lerno.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected BusMessage<TBody> ConstructBusMessage<TBody>(TBody body, BusMessageType messageType)
            where TBody : class
        {
            return new BusMessage<TBody>
            {
                MessageType = messageType,
                Body = body,
            };
        }
    }
}
