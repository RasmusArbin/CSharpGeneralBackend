namespace BackendGeneral.Interfaces
{
    public interface ILogger
    {
        void LogInsert(IIdentifiable entity);

        void LogDelete(IIdentifiable entity);

        void LogUpdate(IIdentifiable entity);

        void Log(string loggingMessage, string stackTrace);
    }
}
