using Strev.QuickTools.DomainModel.Enumeration;
using System;

namespace Strev.QuickTools.DomainModel
{
    public class LogLine
    {
        public LogLine(LogLevel logLevel, Exception exception, string text)
        {
            LogLevel = logLevel;
            Exception = exception;
            Text = text;
        }

        public LogLine(LogLevel logLevel, Exception exception, string pattern, params object[] args)
            : this(logLevel, exception, string.Format(pattern, args))
        {
        }

        public LogLine(LogLevel logLevel, string pattern, params object[] args)
            : this(logLevel, null, string.Format(pattern, args))
        {
        }

        public LogLine(LogLevel logLevel, string text)
            : this(logLevel, null, text)
        {
        }

        public LogLevel LogLevel { get; set; }

        public string Text { get; set; }

        public Exception Exception { get; set; }
    }
}