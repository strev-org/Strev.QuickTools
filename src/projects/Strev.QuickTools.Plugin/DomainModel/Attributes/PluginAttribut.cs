using System;

namespace Strev.QuickTools.Plugin.DomainModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginAttribute : Attribute
    {
        public string[] ParameterNames { get; set; }
    }
}