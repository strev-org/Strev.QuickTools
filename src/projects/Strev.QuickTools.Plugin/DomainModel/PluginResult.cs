namespace Strev.QuickTools.Plugin.DomainModel
{
    public class PluginResult<T>
    {
        /// <summary>
        /// True if the plugin or a plugin handled the resource
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// The result if Handled is true
        /// Could be anything if Handled is false
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// The name of the plugin that handled the result
        /// </summary>
        public string Name { get; set; }
    }
}