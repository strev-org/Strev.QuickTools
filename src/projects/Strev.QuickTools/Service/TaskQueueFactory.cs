using Strev.QuickTools.Core.Service;

namespace Strev.QuickTools.Service
{
    public class TaskQueueFactory : ITaskQueueFactory
    {
        private ILogger Logger { get; set; }
        public IThreadChanger ThreadChanger { get; }

        public TaskQueueFactory(ILogger logger, IThreadChanger threadChanger)
        {
            Logger = logger;
            ThreadChanger = threadChanger;
        }

        public ITaskQueue GetTaskQueue(string name) => new TaskQueue(name, Logger, ThreadChanger);
    }
}