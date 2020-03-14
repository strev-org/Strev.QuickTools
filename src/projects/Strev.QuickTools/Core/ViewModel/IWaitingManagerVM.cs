using System;

namespace Strev.QuickTools.Core.ViewModel
{
    public interface IWaitingManagerVM
    {
        bool IsWaiting { get; set; }
        string Progress { get; }

        void RunAsync(Action<Action<string>> action, Action callback, Action<Exception> onException);

        void RunAsync<TResult>(Func<Action<string>, TResult> action, Action<TResult> callback, Action<Exception> onException);
    }
}