using Strev.QuickTools.DomainModel;
using System;

namespace Strev.QuickTools.Core.Service
{
    public interface ITaskQueue : IDisposable
    {
        void EnqueueTask(QueueTask t);

        void Enqueue(Action action);

        void Enqueue(Action action, Action onTaskTermination, Action<Exception> onEexception);

        bool ForceDispose { get; set; }

        bool IsEmpty { get; }
    }
}