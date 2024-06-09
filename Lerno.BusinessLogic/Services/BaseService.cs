using AutoMapper;
using Lerno.BusinessLogic.Bus;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;

namespace Lerno.BusinessLogic.Services
{
    public abstract class BaseService
    {
        protected readonly IBusMessageQueueService _busMessageService;
        protected readonly IMapper _mapper;

        public BaseService(IBusMessageQueueService busMessageService, IMapper mapper)
        {
            _busMessageService = busMessageService;
            _mapper = mapper;
        }

        protected BusMessage<TBody> ConstructBusMessage<TBody>(TBody body, string action, BusMessageType messageType)
            where TBody : class
        {
            return new BusMessage<TBody>()
            {
                MessageType = messageType,
                Action = action,
                Body = body
            };
        }
    }
}
