using Strev.QuickTools.DomainModel.Enumeration;
using System;

namespace Strev.QuickTools.Core.Service
{
    public interface ILogger : ILoggerImplementation
    {
        void Log(LogLevel logLevel, Exception exception, string text);

        void Log(LogLevel logLevel, Exception exception, string pattern, params object[] args);

        void Log(LogLevel logLevel, string pattern, params object[] args);

        void Log(LogLevel logLevel, string text);
    }
}