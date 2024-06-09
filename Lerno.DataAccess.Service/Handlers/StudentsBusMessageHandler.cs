using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Commands;

namespace Lerno.DataAccess.Service.Handlers
{
    public class StudentsBusMessageHandler : IBusMessageHandler
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsBusMessageHandler(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public TAnswer Handle<TBody, TAnswer>(BusMessage<TBody> busMessage)
            where TBody : class
            where TAnswer : class
        {
            
        }
    }
}
