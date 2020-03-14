using Strev.QuickTools.Core.Service;
using System;
using System.Collections.Generic;

namespace Strev.QuickTools.Service
{
    public class EntityDispatcher<TEntity> : IEntityDispatcher<TEntity>
    {
        private bool _disposed = false;
        private List<Action<TEntity>> _listeners = new List<Action<TEntity>>();

        public EntityDispatcher(IInitDisposeManager initDisposeManager)
        {
            initDisposeManager.AddInitializableDisposable(this);
        }

        public void Dispatch(TEntity entity)
        {
            if (_listeners != null)
            {
                foreach (var listener in _listeners)
                {
                    listener(entity);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EntityDispatcher()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _listeners.Clear();
                    _listeners = null;
                    _disposed = true;
                }
            }
        }

        public void Init()
        {
        }

        public void RegisterListener(Action<TEntity> listener)
        {
            _listeners?.Add(listener);
        }

        public void UnregisterListener(Action<TEntity> listener)
        {
            if (_listeners != null)
            {
                if (_listeners.Contains(listener))
                {
                    _listeners.Remove(listener);
                }
            }
        }
    }
}