using System;

namespace Strev.QuickTools.Core
{
    public interface IInitializable : IDisposable
    {
        void Init();
    }
}