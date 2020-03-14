using System;
using System.ComponentModel;

namespace Strev.QuickTools.Utils
{
    public static class Async
    {
        public static void RunAsync(Action<Action<string>> action, Action callback, Action<Exception> onException, Action<string> onProgress)
        {
            (new BackgroundWrapper<int>((progressNotifier) => { action(progressNotifier); return 0; }, (i) => callback(), onException, onProgress)).Run();
        }

        public static void RunAsync<TResult>(Func<Action<string>, TResult> action, Action<TResult> callback, Action<Exception> onException, Action<string> onProgress)
        {
            (new BackgroundWrapper<TResult>(action, callback, onException, onProgress)).Run();
        }

        private class BackgroundWrapper<TResult>
        {
            private Func<Action<string>, TResult> _action;
            private Action<TResult> _callback;
            private BackgroundWorker _worker;
            private Exception _exception;
            private Action<Exception> _onException;
            private Action<string> _onProgress;

            public BackgroundWrapper(Func<Action<string>, TResult> action, Action<TResult> callback, Action<Exception> onException, Action<string> onProgress)
            {
                _action = action;
                _callback = callback;
                _onException = onException;
                _onProgress = onProgress;
                _exception = null;
                _worker = new BackgroundWorker();
                _worker.WorkerReportsProgress = (onProgress != null);
                _worker.DoWork += _worker_DoWork;
                _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            }

            private void _worker_DoWork(object sender, DoWorkEventArgs e)
            {
                try
                {
                    _worker.ProgressChanged += _worker_ProgressChanged;
                    e.Result = _action(NotifyProgress);
                }
                catch (Exception ex)
                {
                    _exception = ex;
                }
            }

            private void NotifyProgress(string progress)
            {
                _worker.ReportProgress(0, progress);
            }

            private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                if (_onProgress != null && e != null)
                {
                    _onProgress(e.UserState as string);
                }
            }

            private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                try
                {
                    if (_exception != null)
                    {
                        if (_onException != null)
                        {
                            _onException(_exception);
                        }
                        else
                        {
                            throw _exception;
                        }
                    }
                    else
                    {
                        if (_onException != null)
                        {
                            try
                            {
                                _callback((TResult)e.Result);
                            }
                            catch (Exception ex)
                            {
                                _onException(ex);
                            }
                        }
                        else
                        {
                            _callback((TResult)e.Result);
                        }
                    }
                }
                finally
                {
                    _worker.DoWork -= _worker_DoWork;
                    _worker.RunWorkerCompleted -= _worker_RunWorkerCompleted;
                    _action = null;
                    _callback = null;
                    _onException = null;
                    _worker.Dispose();
                    _worker = null;
                }
            }

            public void Run()
            {
                _worker.RunWorkerAsync();
            }
        }
    }
}