using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Plugin.Core.Service;
using Strev.QuickTools.Plugin.DomainModel;

namespace Strev.QuickTools.Plugin.Plugin
{
    public abstract class PluginBase<TConfig, TPlugin> : IPlugin<TConfig>
        where TConfig : IPluginConfig
        where TPlugin : class, IPlugin<TConfig>
    {
        private IInitDisposeManager InitDisposeManager { get; set; }
        private IPluginManager<TConfig, TPlugin> PluginManager { get; set; }
        protected TConfig PluginConfig { get; set; }

        public PluginBase(IInitDisposeManager initDisposeManager, IPluginManager<TConfig, TPlugin> pluginManager)
        {
            InitDisposeManager = initDisposeManager;
            PluginManager = pluginManager;

            InitDisposeManager.AddInitializableDisposable(this);
        }

        public abstract string Name { get; }

        public virtual bool Disabled => false;

        public virtual void OnConfig(TConfig pluginConfig)
        {
            PluginConfig = pluginConfig;
        }

        public virtual void OnPlug()
        {
        }

        public virtual void OnUnplug()
        {
        }

        public virtual void Init()
        {
            PluginManager.Register(this as TPlugin);
        }

        public virtual void Dispose()
        {
            PluginManager.Unregister(this as TPlugin);
        }
    }
}