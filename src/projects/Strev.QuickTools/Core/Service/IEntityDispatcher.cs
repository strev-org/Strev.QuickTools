using System;

namespace Strev.QuickTools.Core.Service
{
    public interface IEntityDispatcher<TEntity> : IInitializable
    {
        void Dispatch(TEntity entity);

        void RegisterListener(Action<TEntity> listener);

        void UnregisterListener(Action<TEntity> listener);
    }
}