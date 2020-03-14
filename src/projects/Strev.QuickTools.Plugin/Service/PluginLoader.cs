using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Plugin.Core.Service;
using Strev.QuickTools.Plugin.DomainModel;
using Strev.QuickTools.Plugin.DomainModel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Strev.QuickTools.Plugin.Service
{
    public class PluginLoader<TConfig, TPlugin> : IPluginLoader<TConfig, TPlugin>
        where TConfig : IPluginConfig
        where TPlugin : class, IPlugin<TConfig>
    {
        private IObjectStore ObjectStore { get; set; }
        private IInitDisposeManager InitDisposeManager { get; set; }

        private IPluginManager<TConfig, TPlugin> PluginManager { get; set; }

        public PluginLoader(IInitDisposeManager initDisposeManager, IObjectStore objectStore, IPluginManager<TConfig, TPlugin> pluginManager)
        {
            InitDisposeManager = initDisposeManager;
            ObjectStore = objectStore;
            PluginManager = pluginManager;
        }

        public void LoadPlugins()
        {
            LoadPlugins(PluginManager.GetType().Assembly);
        }

        private class PluginClass
        {
            public Type Type { get; set; }
            public PluginAttribute PluginAttribute { get; set; }
            public List<Type> DependsOn { get; private set; } = new List<Type>();
        }

        public void LoadPlugins(Assembly assembly)
        {
            List<PluginClass> pluginClassList = new List<PluginClass>();
            foreach (var type in assembly.GetTypes())
            {
                bool isAPlugin = false;
                PluginAttribute pluginAttribute = null;
                var dependsOnAttributes = new List<DependsOnAttribute>();
                foreach (var customAttribute in type.GetCustomAttributes(true))
                {
                    if (customAttribute is PluginAttribute)
                    {
                        isAPlugin = true;
                        pluginAttribute = customAttribute as PluginAttribute;
                    }
                    if (customAttribute is DependsOnAttribute)
                    {
                        dependsOnAttributes.Add(customAttribute as DependsOnAttribute);
                    }
                }
                if (isAPlugin)
                {
                    if (typeof(TPlugin).IsAssignableFrom(type))
                    {
                        var pluginClass = new PluginClass
                        {
                            Type = type,
                            PluginAttribute = pluginAttribute,
                        };
                        pluginClassList.Add(pluginClass);
                        foreach (var dependsOnAttribute in dependsOnAttributes)
                        {
                            pluginClass.DependsOn.Add(dependsOnAttribute.Type);
                        }
                    }
                }
            }
            pluginClassList = ResolvPluginClassOrder(pluginClassList);
            foreach (var pluginClass in pluginClassList)
            {
                LoadPlugin(pluginClass.Type, pluginClass.PluginAttribute);
            }
        }

        private List<PluginClass> ResolvPluginClassOrder(List<PluginClass> pluginClassList)
        {
            var pluginClassListResult = new List<PluginClass>();
            var pluginClassesResultByType = new Dictionary<Type, PluginClass>();
            var pluginClassListRemaining = pluginClassList.ToList();
            bool hasRemovedElements = true;
            while (hasRemovedElements)
            {
                hasRemovedElements = false;
                foreach (var pluginClass in pluginClassListRemaining.ToList())
                {
                    bool canLoadPlugin = pluginClass.DependsOn.All(type => pluginClassesResultByType.ContainsKey(type));
                    if (canLoadPlugin)
                    {
                        pluginClassListResult.Add(pluginClass);
                        pluginClassesResultByType[pluginClass.Type] = pluginClass;
                        pluginClassListRemaining.Remove(pluginClass);
                        hasRemovedElements = true;
                    }
                }
            }
            if (pluginClassListRemaining.Count > 0)
            {
                throw new Exception("Circular reference between plugins");
            }
            return pluginClassListResult;
        }

        private void LoadPlugin(Type type, PluginAttribute pluginAttribute)
        {
            List<object> args = new List<object>();
            args.Add(InitDisposeManager);
            args.Add(PluginManager);
            var names = pluginAttribute?.ParameterNames;
            if (names != null && names.Length > 0)
            {
                foreach (var name in names)
                {
                    args.Add(ObjectStore.GetObject<object>(name));
                }
            }

            var instance = Activator.CreateInstance(type, args.ToArray()) as TPlugin;
            ObjectStore.RegisterObject(instance.Name + "Plugin", instance);
        }
    }
}