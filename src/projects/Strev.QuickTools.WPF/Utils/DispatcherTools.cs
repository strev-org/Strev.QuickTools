using Strev.QuickTools.Core.Service;
using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Windows.Threading;

namespace Strev.QuickTools.Utils
{
    public static class DispatcherTools
    {
        /// <summary>
        /// Pump messages and execute callbacks dispatched on the dispatcher of the current thread for the
        /// specified amount of time.
        /// </summary>
        /// <param name="timeoutMilliseconds">Duration of message pumping, in milliseconds</param>
        public static void PumpMessages(int timeoutMilliseconds, ILogger logger)
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            Dispatcher dispatcher;
            try
            {
                dispatcher = Dispatcher.CurrentDispatcher;
            }
            catch (InvalidOperationException)
            {
                return;
            }

            var frame = new DispatcherFrame();

            // The DispatcherTimer will be invoked after timeout on the
            // current dispatcher to stop the DispatcherFrame.
            // The flag DispatcherPriority.ApplicationIdle is used to be sure that
            // it will be called after every other waiting messages.
            var timerArretPompe = new DispatcherTimer(
                timeout,
                DispatcherPriority.ApplicationIdle,
                (sender, args) =>
                {
                    ((DispatcherTimer)sender).Stop();
                    frame.Continue = false;
                },
                dispatcher);

            timerArretPompe.Start();
            try
            {
                Dispatcher.PushFrame(frame);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Warn, ex, "An exception occured when pushing frame in PumpMessage");
            }
        }
    }
}