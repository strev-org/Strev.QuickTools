namespace Strev.QuickTools.Core.Service
{
    public interface ITaskQueueFactory
    {
        ITaskQueue GetTaskQueue(string name);
    }
}