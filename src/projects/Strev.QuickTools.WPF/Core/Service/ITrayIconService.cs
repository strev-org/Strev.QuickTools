namespace Strev.QuickTools.Core.Service
{
    /// <summary>
    /// This service handle the tray icon
    /// </summary>
    public interface ITrayIconService
    {
        /// <summary>
        /// Hide the main window of the application in the tray icon
        /// </summary>
        void Hide();

        /// <summary>
        /// Show the main window of the application, hidden in the tray icon
        /// </summary>
        void Show();

        /// <summary>
        /// True if the main window of the application is shown and not in the tray icon
        /// </summary>
        bool Shown { get; }

        /// <summary>
        /// True if the tray icon has been installed
        /// </summary>
        bool Installed { get; }

        /// <summary>
        /// Install the tray icon
        /// </summary>
        void Install();

        /// <summary>
        /// Uninstall the tray icon
        /// </summary>
        void Uninstall();
    }
}