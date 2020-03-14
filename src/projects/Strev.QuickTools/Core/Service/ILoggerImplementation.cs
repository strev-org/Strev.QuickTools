using Strev.QuickTools.DomainModel;

namespace Strev.QuickTools.Core.Service
{
    public interface ILoggerImplementation
    {
        void Log(LogLine logLine);
    }
}