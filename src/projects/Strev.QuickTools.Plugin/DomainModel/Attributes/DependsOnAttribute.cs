using System;

namespace Strev.QuickTools.Plugin.DomainModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}