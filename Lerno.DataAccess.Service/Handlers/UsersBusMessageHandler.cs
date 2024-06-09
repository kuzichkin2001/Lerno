using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Commands;

namespace Lerno.DataAccess.Service.Handlers
{
    public class UsersBusMessageHandler : IBusMessageHandler
    {
        private readonly IUsersRepository _usersRepository;

        public UsersBusMessageHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public TAnswer Handle<TBody, TAnswer>(BusMessage<TBody> busMessage)
            where TBody : class
            where TAnswer : class
        {
            
        }
    }
}
