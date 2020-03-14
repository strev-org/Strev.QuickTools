using Strev.QuickTools.Core.Service;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Strev.QuickTools.Service
{
    public class TrayIconService : ITrayIconService
    {
        public bool Installed { get; private set; } = false;

        public bool Shown { get; private set; } = true;

        private Window Window { get; set; }

        private NotifyIcon NotifyIcon { get; set; }

        public void Install()
        {
            Window = System.Windows.Application.Current.MainWindow;
            if (Window != null)
            {
                NotifyIcon = new NotifyIcon();
                using (var iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/MoMock.ico"))?.Stream)
                {
                    if (iconStream != null)
                    {
                        NotifyIcon.Icon = new Icon(iconStream);
                    }
                }
                // NotifyIcon.Icon = ConvertImageResourceToIcon(Window.Icon);
                Window.StateChanged += MainWindow_StateChanged;
                NotifyIcon.Click += NotifyIcon_Click;
                if (Window.IsLoaded)
                {
                    NotifyIcon.Visible = true;
                }
                else
                {
                    Window.Loaded += (s, e) =>
                    {
                        NotifyIcon.Visible = true;
                    };
                }
            }
        }

        // ReSharper disable once UnusedMember.Local
        // In case that method is needed
        private Icon ConvertImageResourceToIcon(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }
            if (imageSource.GetType().FullName != @"System.Windows.Media.Imaging.BitmapFrameDecode")
            {
                return null;
            }

            var uri = new Uri(imageSource.ToString());
            var streamInfo = System.Windows.Application.GetResourceStream(uri);

            if (streamInfo == null)
            {
                return null;
            }

            return new Icon(streamInfo.Stream);
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (Shown)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (Window.WindowState == WindowState.Minimized)
            {
                Hide();
            }
        }

        private void InternalShow()
        {
            Window.WindowState = WindowState.Normal;
            Window.Show();
            Shown = true;
            Window.Activate();
            Window.Focus();
        }

        public void Show()
        {
            if (Window.IsLoaded)
            {
                InternalShow();
            }
            else
            {
                Window.Loaded += (s, e) => InternalShow();
            }
        }

        private void InternalHide()
        {
            Window.Hide();
            Window.WindowState = WindowState.Minimized;
            Shown = false;
        }

        public void Hide()
        {
            if (Window.IsLoaded)
            {
                InternalHide();
            }
            else
            {
                Window.WindowState = WindowState.Minimized;
                Window.Loaded += (s, e) =>
                {
                    InternalHide();
                };
            }
        }

        public void Uninstall()
        {
            if (NotifyIcon != null)
            {
                NotifyIcon.DoubleClick -= NotifyIcon_Click;
                NotifyIcon.Dispose();
                NotifyIcon = null;
            }
            if (Window != null)
            {
                Window.StateChanged -= MainWindow_StateChanged;
            }
        }
    }
}