using Strev.QuickTools.Core.Service;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Strev.QuickTools.Service
{
    public class SerializerService<T> : ISerializerService<T>
    {
        private XmlSerializer _serializer;

        private XmlSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new XmlSerializer(typeof(T));
                }
                return _serializer;
            }
        }

        private string NormalizeFilename(string filename, string extension)
        {
            string fullFilename = filename;
            if (!fullFilename.ToLower().EndsWith(extension.ToLower(), StringComparison.Ordinal))
            {
                fullFilename = fullFilename + extension;
            }
            return fullFilename;
        }

        public void SaveEntity(string filename, T entity)
        {
            string fullFilename = NormalizeFilename(filename, ".xml");
            var dirname = Path.GetDirectoryName(fullFilename);
            if (dirname != null)
            {
                if (!Directory.Exists(dirname))
                {
                    Directory.CreateDirectory(dirname);
                }
            }
            using (var writer = new StreamWriter(fullFilename, false, Encoding.UTF8))
            {
                Serializer.Serialize(writer, entity);
            }
        }

        public T ReadEntity(string filename, Func<T> defaultGetter, Action<T> onLoaded, bool createIfNotThere)
        {
            string fullFilename = NormalizeFilename(filename, ".xml");
            if (File.Exists(fullFilename))
            {
                using (var reader = new StreamReader(fullFilename, Encoding.UTF8))
                {
                    var entity = (T)Serializer.Deserialize(reader);
                    onLoaded?.Invoke(entity);
                    return entity;
                }
            }
            else
            {
                var entity = defaultGetter();
                if (createIfNotThere)
                {
                    SaveEntity(fullFilename, entity);
                }
                onLoaded?.Invoke(entity);
                return entity;
            }
        }
    }
}