using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Plugin.Core.Service;
using Strev.QuickTools.Plugin.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strev.QuickTools.Plugin.Service
{
    public abstract class PluginManager<TConfig, TPlugin> : IPluginManager<TConfig, TPlugin>, IDisposable
        where TConfig : IPluginConfig
        where TPlugin : IPlugin<TConfig>
    {
        protected IInitDisposeManager InitDisposeManager { get; set; }

        private List<TPlugin> Plugins { get; } = new List<TPlugin>();

        protected abstract TConfig PluginConfig { get; }

        public PluginManager(IInitDisposeManager initDisposeManager)
        {
            InitDisposeManager = initDisposeManager;
            InitDisposeManager.AddDisposable(this);
        }

        #region Register/Unregister

        public void Register(TPlugin plugin)
        {
            if (IsPluginCallable(plugin))
            {
                Plugins.Add(plugin);
                plugin.OnConfig(PluginConfig);
                plugin.OnPlug();
            }
        }

        public void Unregister(TPlugin plugin)
        {
            if (Plugins.Contains(plugin))
            {
                plugin.OnUnplug();
                Plugins.Remove(plugin);
            }
        }

        #endregion Register/Unregister

        protected abstract bool IsPluginCallable(TPlugin plugin);

        protected abstract bool IsPluginCallable<TTarget>(TPlugin plugin, TTarget obj);

        #region Iteration on plugins

        protected PluginResult<T> HandleCode<T>(Func<TPlugin, PluginResult<T>> code)
        {
            return HandleCode<T, object>(null, code);
        }

        protected PluginResult<T> HandleCode<T, TTarget>(TTarget target, Func<TPlugin, PluginResult<T>> code)
        {
            foreach (var plugin in Plugins)
            {
                if (IsPluginCallable<TTarget>(plugin, target))
                {
                    var pluginResult = code(plugin);
                    if (pluginResult != null && pluginResult.Handled)
                    {
                        pluginResult.Name = plugin.Name;
                        return pluginResult;
                    }
                }
            }

            return new PluginResult<T> { Handled = false };
        }

        protected PluginResult<T> PatchCode<T, TTarget>(TTarget target, Func<TPlugin, T, PluginResult<T>> code, T value)
        {
            var currentValue = value;
            foreach (var plugin in Plugins)
            {
                if (IsPluginCallable<TTarget>(plugin, target))
                {
                    var pluginResult = code(plugin, currentValue);
                    if (pluginResult != null && pluginResult.Handled)
                    {
                        currentValue = pluginResult.Result;
                    }
                }
            }
            return new PluginResult<T> { Handled = true, Name = null, Result = currentValue };
        }

        protected void OnCode(Action<TPlugin> action)
        {
            foreach (var plugin in Plugins)
            {
                if (IsPluginCallable(plugin))
                {
                    action(plugin);
                }
            }
        }

        #endregion Iteration on plugins

        public void Dispose()
        {
            foreach (var plugin in Plugins.ToList())
            {
                Unregister(plugin);
            }
        }
    }
}