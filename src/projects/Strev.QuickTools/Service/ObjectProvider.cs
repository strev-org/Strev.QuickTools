using Strev.QuickTools.Core.Service;
using System.Collections.Generic;

namespace Strev.QuickTools.Service
{
    public class ObjectProvider : IObjectProvider, IObjectStore
    {
        private Dictionary<string, object> Objects { get; set; } = new Dictionary<string, object>();

        public ObjectProvider()
        {
        }

        public T GetObject<T>(string name)
            where T : class
        {
            return Objects[name] as T;
        }

        public T RegisterObject<T>(string name, T obj)
        {
            Objects[name] = obj;
            return obj;
        }
    }
}