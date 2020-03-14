using System;

namespace Strev.QuickTools.Core.Service
{
    public interface IThreadChanger : IInitializable
    {
        void EnqueueInUIThread(Action action);

        void InvokeInUIThread(Action action);

        T InvokeInUIThread<T>(Func<T> func);

        ISubThreadChanger GetSubThreadChanger();

        void PumpMessages(int timeoutMilliseconds, ILogger logger);
    }
}