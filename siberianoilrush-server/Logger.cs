using Microsoft.Extensions.Logging;
using siberianoilrush_server.Service;
using System;

namespace siberianoilrush_server
{
    public class Logger : ILogger
    {
        private readonly LoggerService _loggerService;

        public Logger(LoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel >= LogLevel.Warning)
                return true;

            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel >= LogLevel.Warning)
                if (formatter != null)
                    _loggerService.Log(exception?.Message, exception, logLevel);
        }
    }
}
