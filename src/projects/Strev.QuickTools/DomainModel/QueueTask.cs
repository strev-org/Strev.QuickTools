using System;

namespace Strev.QuickTools.DomainModel
{
    public class QueueTask
    {
        /// <summary>
        /// Action to execute
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Callback to call when the task has finished.
        /// The callback may be dispatched on the main thread if the TaskQueue is configured
        /// </summary>
        public Action OnTaskTermination { get; set; }

        /// <summary>
        /// Exception handler to call if an exception occured during execution of the task.
        /// </summary>
        public Action<Exception> OnException { get; set; }
    }
}