using Strev.QuickTools.Plugin.DomainModel;

namespace Strev.QuickTools.Plugin.Core.Service
{
    public interface IPluginManager<TConfig, TPlugin>
        where TConfig : IPluginConfig
        where TPlugin : IPlugin<TConfig>
    {
        void Register(TPlugin plugin);

        void Unregister(TPlugin plugin);
    }
}