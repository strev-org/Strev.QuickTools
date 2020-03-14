using System;
using System.Text;
using System.Windows.Threading;

namespace Strev.QuickTools.ViewModel.Helper
{
    public static class DispatcherHelper
    {
        public static Dispatcher UIDispatcher { get; private set; }

        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (action == null)
            {
                return;
            }

            CheckDispatcher();

            if (UIDispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                UIDispatcher.BeginInvoke(action);
            }
        }

        private static void CheckDispatcher()
        {
            if (UIDispatcher == null)
            {
                var error = new StringBuilder("The DispatcherHelper is not initialized.");
                error.AppendLine();
                error.Append("Call DispatcherHelper.Initialize() in the static App constructor.");

                throw new InvalidOperationException(error.ToString());
            }
        }

        public static DispatcherOperation RunAsync(Action action)
        {
            CheckDispatcher();

            return UIDispatcher.BeginInvoke(action);
        }

        public static void Initialize()
        {
            if (UIDispatcher != null && UIDispatcher.Thread.IsAlive)
            {
                return;
            }

            UIDispatcher = Dispatcher.CurrentDispatcher;
        }

        public static void Reset()
        {
            UIDispatcher = null;
        }
    }
}