using System;
using UnoWebApiSwagger.ClientContracts;

namespace ButchersQA.Uwp
{
    internal class LogService : ILogService
    {
        public void LogDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void LogError(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void LogInfo(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}