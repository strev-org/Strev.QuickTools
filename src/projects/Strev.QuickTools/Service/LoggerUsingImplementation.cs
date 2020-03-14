using Strev.QuickTools.Core.Service;
using Strev.QuickTools.DomainModel;
using Strev.QuickTools.DomainModel.Enumeration;
using System;

namespace Strev.QuickTools.Service
{
    public class LoggerUsingLoggerImplementation : ILogger
    {
        private ILoggerImplementation LoggerImplementation { get; set; }

        public LoggerUsingLoggerImplementation(ILoggerImplementation loggerImplementation)
        {
            LoggerImplementation = loggerImplementation;
        }

        public void Log(LogLevel logLevel, Exception exception, string text)
        {
            Log(new LogLine(logLevel, exception, text));
        }

        public void Log(LogLevel logLevel, Exception exception, string pattern, params object[] args)
        {
            Log(new LogLine(logLevel, exception, pattern, args));
        }

        public void Log(LogLevel logLevel, string pattern, params object[] args)
        {
            Log(new LogLine(logLevel, pattern, args));
        }

        public void Log(LogLevel logLevel, string text)
        {
            Log(new LogLine(logLevel, text));
        }

        public void Log(LogLine logLine)
        {
            LoggerImplementation.Log(logLine);
        }
    }
}