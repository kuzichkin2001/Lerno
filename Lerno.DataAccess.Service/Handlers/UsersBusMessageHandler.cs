using Lerno.DataAccess.Interfaces;

namespace Lerno.DataAccess.Service.Handlers
{
    public class UsersBusMessageHandler : IBusMessageHandler
    {
        private readonly IUsersRepository _usersRepository;

        public TAnswer Handle<TBody, TAnswer>(string action, TBody body)
            where TBody : class
            where TAnswer : class
        {
            
        }
    }
}
