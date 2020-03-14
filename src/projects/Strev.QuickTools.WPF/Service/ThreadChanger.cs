using Strev.QuickTools.Core.Service;
using System;
using System.Windows.Threading;

namespace Strev.QuickTools.Service
{
    public class ThreadChanger : IThreadChanger
    {
        protected Dispatcher UIDispatcher { get; private set; }
        public IInitDisposeManager InitDisposeManager { get; }

        public ThreadChanger(IInitDisposeManager initDisposeManager)
        {
            InitDisposeManager = initDisposeManager;
            InitDisposeManager.AddInitializableDisposable(this);
        }

        public void Init()
        {
            if (UIDispatcher != null && UIDispatcher.Thread.IsAlive)
            {
                return;
            }
            UIDispatcher = Dispatcher.CurrentDispatcher;
        }

        public void Dispose()
        {
            UIDispatcher = null;
        }

        public void EnqueueInThread(Action action, Dispatcher dispatcher) => dispatcher.BeginInvoke(action);

        public void InvokeInThread(Action action, Dispatcher dispatcher) => dispatcher.Invoke(action);

        public T InvokeInThread<T>(Func<T> func, Dispatcher dispatcher)
        {
            T result = default(T);
            var action = new Action(() => { result = func(); });
            InvokeInThread(action, dispatcher);
            return result;
        }

        public void EnqueueInUIThread(Action action) => EnqueueInThread(action, UIDispatcher);

        public void InvokeInUIThread(Action action) => InvokeInThread(action, UIDispatcher);

        public T InvokeInUIThread<T>(Func<T> func) => InvokeInThread(func, UIDispatcher);

        public ISubThreadChanger GetSubThreadChanger()
        {
            return new SubThreadChanger(this);
        }

        public void PumpMessages(int timeoutMilliseconds, ILogger logger) => PumpMessages(timeoutMilliseconds, logger, UIDispatcher);

        public void PumpMessages(int timeoutMilliseconds, ILogger logger, Dispatcher dispatcher)
        {
        }
    }
}