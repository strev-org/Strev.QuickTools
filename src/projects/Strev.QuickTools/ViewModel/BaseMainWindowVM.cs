using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Core.ViewModel;
using Strev.QuickTools.Utils;
using System;

namespace Strev.QuickTools.ViewModel
{
    public abstract class BaseMainWindowVM : BaseScreenContainerVM, IWaitingManagerVM, IMainWindowVM
    {
        protected BaseMainWindowVM(IInitDisposeManager initDisposeManager, IScreenContainerVM parent)
            : base(initDisposeManager, parent)
        {
        }

        private bool _isWaiting;

        public bool IsWaiting
        {
            get
            {
                return _isWaiting;
            }
            set
            {
                if (_isWaiting == value) return;
                _isWaiting = value;
                OnPropertyChanged("IsWaiting");
            }
        }

        public void RunAsync(Action<Action<string>> action, Action callback, Action<Exception> onException)
        {
            IsWaiting = true;
            Progress = string.Empty;
            Async.RunAsync(
                action,
                () =>
                {
                    callback();
                    IsWaiting = false;
                },
                (e) =>
                {
                    onException?.Invoke(e);
                    IsWaiting = false;
                },
                ShowProgress);
        }

        public void RunAsync<TResult>(Func<Action<string>, TResult> action, Action<TResult> callback, Action<Exception> onException)
        {
            IsWaiting = true;
            Progress = string.Empty;
            Async.RunAsync<TResult>(
                action,
                (r) =>
                {
                    callback(r);
                    IsWaiting = false;
                },
                (e) =>
                {
                    onException?.Invoke(e);
                    IsWaiting = false;
                },
                ShowProgress);
        }

        private string _progress = null;

        public string Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged("Progress");
                }
            }
        }

        public void ShowProgress(string progressInfo)
        {
            Progress = progressInfo;
        }

        public override IWaitingManagerVM WaitingManagerVM => this;

        public override IScreenContainerVM RootScreen => this;
    }
}