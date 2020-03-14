using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace Strev.QuickTools.View.UserControls
{
    /// <summary>
    /// Interaction logic for RounderProgressBarControl.xaml
    /// </summary>
    public partial class RounderProgressBarControl : UserControl
    {
        private Timer _timer;

        public RounderProgressBarControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _timer = new Timer(100);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.Closing += (s, evt) =>
                {
                    OnUnloaded(null, null);
                };
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Close();
                _timer.Dispose();
                _timer = null;
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            rotationCanvas.Dispatcher.Invoke(new Action(OnTimer), null);
        }

        private void OnTimer()
        {
            SpinnerRotate.Angle += 30;
            if (Math.Abs(SpinnerRotate.Angle - 360) < 1)
            {
                SpinnerRotate.Angle = 0;
            }
        }
    }
}