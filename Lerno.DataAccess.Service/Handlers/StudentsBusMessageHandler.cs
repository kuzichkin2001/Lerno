using Lerno.DataAccess.Interfaces;

namespace Lerno.DataAccess.Service.Handlers
{
    public class StudentsBusMessageHandler : IBusMessageHandler
    {
        private readonly IStudentsRepository _studentsRepository;

        public TAnswer Handle<TBody, TAnswer>(string action, TBody body)
            where TBody : class
            where TAnswer : class
        {
            
        }
    }
}
