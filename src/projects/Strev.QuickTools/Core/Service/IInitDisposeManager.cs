using System;

namespace Strev.QuickTools.Core.Service
{
    public interface IInitDisposeManager : IInitializable
    {
        void AddInitializableDisposable(IInitializable initializable);

        void AddInitializable(IInitializable disposable);

        void AddDisposable(IDisposable disposable);
    }
}