using System.Windows;

namespace Strev.QuickTools.MainApp
{
    /// <summary>
    /// Interaction logic for BaseApp.xaml
    /// </summary>
    public class BaseApp<TS> : Application
        where TS : BaseStarter, new()
    {
        private BaseStarter _starter;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _starter = new TS
            {
                App = this
            };
            _starter.Start(Shutdown);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _starter.Stop();
            base.OnExit(e);
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            _starter.Stop();
        }

        public static void MainEntryPoint()
        {
            var app = new BaseApp<TS>();
            app.Run();
        }
    }
}