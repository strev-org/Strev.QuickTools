using Strev.QuickTools.Core.Service;
using Strev.QuickTools.DomainModel;
using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Strev.QuickTools.Service
{
    public class TaskQueue : ITaskQueue
    {
        private readonly EventWaitHandle _wh = new AutoResetEvent(false);

        private readonly Thread _worker;
        private readonly object _locker = new object();
        private readonly Queue<QueueTask> _tasks = new Queue<QueueTask>();
        private const string _threadName = "TaskQueue thread - ";

        private ILogger Logger { get; set; }
        public IThreadChanger ThreadChanger { get; }
        public ISubThreadChanger SubThreadChanger { get; set; }

        public TaskQueue(string name, ILogger logger, IThreadChanger threadChanger)
            : this(name, null, logger, threadChanger)
        {
        }

        public TaskQueue(string name, ApartmentState apartmentState, ILogger logger, IThreadChanger threadChanger)
            : this(name, new ApartmentState?(apartmentState), logger, threadChanger)
        {
        }

        private TaskQueue(string name, ApartmentState? apartmentState, ILogger logger, IThreadChanger threadChanger)
        {
            Logger = logger;
            ThreadChanger = threadChanger;
            _worker = new Thread(Work)
            {
                IsBackground = true,
                Name = _threadName + name
            };
            if (apartmentState.HasValue)
            {
                _worker.SetApartmentState(apartmentState.Value);
            }
            _worker.Start();
        }

        public void EnqueueTask(QueueTask t)
        {
            lock (_locker)
            {
                _tasks.Enqueue(t);
            }

            _wh.Set();
        }

        public void Enqueue(Action action) => EnqueueTask(new QueueTask { Action = action });

        public void Enqueue(Action action, Action onTaskTerminationCallback, Action<Exception> exceptionHandler) => EnqueueTask(new QueueTask { Action = action, OnTaskTermination = onTaskTerminationCallback, OnException = exceptionHandler });

        /// <summary>
        /// When set to true, disposes the queue without waiting for all tasks be done.
        /// </summary>
        public bool ForceDispose { get; set; }

        /// <summary>
        /// Return true if the task queue is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock (_locker)
                {
                    return !_tasks.Any();
                }
            }
        }

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                if (ForceDispose)
                {
                    _tasks.Clear();
                }
                EnqueueTask(null); // null will signal the consumer to exit.

                // Wait for the thread to Stop
                while (!_worker.Join(100))
                {
                    SubThreadChanger.PumpMessages(100, Logger);
                }

                _wh.Close(); // Release any OS resources.
                _disposed = true;
            }
        }

        ~TaskQueue()
        {
            try
            {
                Dispose();
            }
            catch (Exception)
            {
                // Not the good place to log, logging API may already have been disposed.
            }
        }

        #endregion IDisposable

        #region Background thread work

        private void Work()
        {
            try
            {
                SubThreadChanger = ThreadChanger.GetSubThreadChanger();
                while (true)
                {
                    QueueTask task = null;

                    lock (_locker)
                    {
                        if (_tasks.Count > 0)
                        {
                            task = _tasks.Dequeue();
                            if (task == null)  // empty task = signal for stop
                            {
                                return;
                            }
                        }
                    }

                    if (task != null)
                    {
                        DoTask(task);
                    }
                    else
                    {
                        // Queue is empty, waiting for a new task
                        _wh.WaitOne();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Warn, ex, "An abnormal exception occurs in a TaskQueue that has not been catched properly");
            }
        }

        private void DoTask(QueueTask task)
        {
            string taskname = null;

            bool exception = false;

            try
            {
                taskname = task.Action.Method.Name;
                task.Action();
            }
            catch (Exception e)
            {
                exception = true;

                if (task.OnException != null)
                {
                    SubThreadChanger.InvokeInLocalThread(() => task.OnException(e));
                }
                else
                {
                    Logger.Log(LogLevel.Error, e, "Async task failed, with Exception");
                    throw;
                }
            }

            Logger.Log(LogLevel.Debug, "Async task finished : {0}", taskname);

            if (task.OnTaskTermination != null && !exception)
            {
                SubThreadChanger.InvokeInLocalThread(task.OnTaskTermination);
            }
        }

        #endregion Background thread work
    }
}