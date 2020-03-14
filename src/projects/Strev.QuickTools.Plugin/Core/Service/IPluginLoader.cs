using Strev.QuickTools.Plugin.DomainModel;
using System.Reflection;

namespace Strev.QuickTools.Plugin.Core.Service
{
    public interface IPluginLoader<TConfig, TPlugin>
        where TConfig : IPluginConfig
        where TPlugin : class, IPlugin<TConfig>
    {
        void LoadPlugins(Assembly assembly);

        void LoadPlugins();
    }
}