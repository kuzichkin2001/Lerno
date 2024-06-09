namespace Lerno.DataAccess.Service.Handlers
{
    public interface IBusMessageHandler
    {
        TAnswer Handle<TBody, TAnswer>(string action, TBody body)
            where TBody : class
            where TAnswer : class;
    }
}
