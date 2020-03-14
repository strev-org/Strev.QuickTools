using Strev.QuickTools.Core;
using Strev.QuickTools.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strev.QuickTools.Service
{
    public class InitDisposeManager : IInitDisposeManager
    {
        private Stack<IInitializable> _initializables = new Stack<IInitializable>();
        private Stack<IDisposable> _disposables = new Stack<IDisposable>();

        private bool _hasBeenInitialized = false;

        public void AddDisposable(IDisposable disposable)
        {
            _disposables?.Push(disposable);
        }

        public void AddInitializable(IInitializable initializable)
        {
            _initializables?.Push(initializable);
            if (_hasBeenInitialized)
            {
                initializable.Init();
            }
        }

        public void AddInitializableDisposable(IInitializable initializable)
        {
            AddInitializable(initializable);
            AddDisposable(initializable);
        }

        public void Init()
        {
            if (_initializables != null)
            {
                var initializablesHashSet = new HashSet<IInitializable>();

                bool remainingInitToDo;
                do
                {
                    remainingInitToDo = false;
                    var initializables = _initializables.Reverse().ToList();
                    foreach (var initializable in initializables)
                    {
                        if (initializablesHashSet.Contains(initializable)) continue;
                        initializablesHashSet.Add(initializable);
                        initializable.Init();
                        remainingInitToDo = true;
                    }
                } while (remainingInitToDo);
                _hasBeenInitialized = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~InitDisposeManager()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_initializables != null)
                {
                    _initializables.Clear();
                    _initializables = null;
                }

                if (_disposables != null)
                {
                    while (_disposables.Count > 0)
                    {
                        var disposable = _disposables.Pop();
                        //try
                        //{
                        disposable.Dispose();
                        //}
                        //catch
                        //{
                        //    // Better safe than sorry
                        //}
                    }
                    _disposables = null;
                }
            }
        }
    }
}