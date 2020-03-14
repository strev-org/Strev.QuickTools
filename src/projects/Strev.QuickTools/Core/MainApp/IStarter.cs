using System;

namespace Strev.QuickTools.Core.MainApp
{
    public interface IStarter
    {
        void Start(Action onStop);

        void Stop();
    }
}