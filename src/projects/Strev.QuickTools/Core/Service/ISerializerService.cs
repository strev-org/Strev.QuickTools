using System;

namespace Strev.QuickTools.Core.Service
{
    public interface ISerializerService<T>
    {
        void SaveEntity(string fullFilename, T entity);

        T ReadEntity(string fullFilename, Func<T> defaultGetter, Action<T> onLoaded, bool createIfNotThere);
    }
}