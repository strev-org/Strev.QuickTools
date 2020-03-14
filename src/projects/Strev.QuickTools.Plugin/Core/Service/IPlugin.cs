using Strev.QuickTools.Core;
using Strev.QuickTools.Plugin.DomainModel;

namespace Strev.QuickTools.Plugin.Core.Service
{
    public interface IPlugin<TConfig> : IInitializable where TConfig : IPluginConfig
    {
        /// <summary>
        /// Le nom du plugin
        /// </summary>

        string Name { get; }

        bool Disabled { get; }

        void OnConfig(TConfig pluginConfig);

        void OnPlug();

        void OnUnplug();
    }
}