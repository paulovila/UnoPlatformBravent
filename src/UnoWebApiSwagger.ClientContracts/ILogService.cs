using System;

namespace UnoWebApiSwagger.ClientContracts
{
    public interface ILogService
    {
        void LogInfo(string message);
        void LogDebug(string message);
        void LogError(Exception exception);
    }
}
