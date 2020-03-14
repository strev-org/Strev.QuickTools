namespace Strev.QuickTools.Core.Service
{
    public interface IObjectProvider
    {
        T GetObject<T>(string name) where T : class;
    }
}