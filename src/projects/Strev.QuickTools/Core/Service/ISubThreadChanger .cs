using System;

namespace Strev.QuickTools.Core.Service
{
    public interface ISubThreadChanger : IThreadChanger
    {
        void EnqueueInLocalThread(Action action);

        void InvokeInLocalThread(Action action);

        T InvokeInLocalThread<T>(Func<T> func);
    }
}