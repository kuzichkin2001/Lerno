namespace Lerno.DataAccess.Service.Handlers
{
    public class TeacherBusMessageHandler : IBusMessageHandler
    {
        public TAnswer Handle<TBody, TAnswer>(string action, TBody body)
            where TBody : class
            where TAnswer : class
        {
            
        }
    }
}
