using Lerno.DataAccess.DbContexts;
using Lerno.DataAccess.Interfaces;

namespace Lerno.DataAccess.Service.Handlers
{
    public class TeacherBusMessageHandler : IBusMessageHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeachersRepository _teachersRepository;

        public TeacherBusMessageHandler(ITeachersRepository teachersRepository, IUnitOfWork unitOfWork)
        {
            _teachersRepository = teachersRepository;
            _unitOfWork = unitOfWork;
        }

        public TAnswer Handle<TBody, TAnswer>(string action, TBody body)
            where TBody : class
            where TAnswer : class
        {
            
        }
    }
}
