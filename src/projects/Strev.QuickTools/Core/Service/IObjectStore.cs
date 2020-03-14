namespace Strev.QuickTools.Core.Service
{
    public interface IObjectStore : IObjectProvider
    {
        T RegisterObject<T>(string name, T obj);
    }
}