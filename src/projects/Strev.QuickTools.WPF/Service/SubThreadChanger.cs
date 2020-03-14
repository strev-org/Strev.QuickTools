using Strev.QuickTools.Core.Service;
using System;
using System.Windows.Threading;

namespace Strev.QuickTools.Service
{
    public class SubThreadChanger : ISubThreadChanger
    {
        private ThreadChanger ThreadChanger { get; }
        protected Dispatcher LocalDispatcher { get; private set; }

        public SubThreadChanger(ThreadChanger threadChanger)
        {
            ThreadChanger = threadChanger;
        }

        public void Init()
        {
            if (LocalDispatcher != null && LocalDispatcher.Thread.IsAlive)
            {
                return;
            }
            LocalDispatcher = Dispatcher.CurrentDispatcher;
        }

        public void Dispose()
        {
            LocalDispatcher = null;
        }

        public void EnqueueInLocalThread(Action action) => ThreadChanger.EnqueueInThread(action, LocalDispatcher);

        public void EnqueueInUIThread(Action action) => ThreadChanger.EnqueueInUIThread(action);

        public ISubThreadChanger GetSubThreadChanger() => ThreadChanger.GetSubThreadChanger();

        public void InvokeInLocalThread(Action action) => ThreadChanger.InvokeInThread(action, LocalDispatcher);

        public T InvokeInLocalThread<T>(Func<T> func) => ThreadChanger.InvokeInThread(func, LocalDispatcher);

        public void InvokeInUIThread(Action action) => ThreadChanger.InvokeInUIThread(action);

        public T InvokeInUIThread<T>(Func<T> func) => ThreadChanger.InvokeInUIThread(func);

        public void PumpMessages(int timeoutMilliseconds, ILogger logger) => ThreadChanger.PumpMessages(timeoutMilliseconds, logger, LocalDispatcher);
    }
}