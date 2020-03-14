using Strev.QuickTools.Core.MainApp;
using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Core.ViewModel;
using Strev.QuickTools.Service;
using Strev.QuickTools.View;
using System;
using System.Windows;

namespace Strev.QuickTools.MainApp
{
    public abstract class BaseStarter : IStarter
    {
        public Application App { get; set; }

        protected Action OnStop { get; set; } = null;

        protected IInitDisposeManager InitDisposeManager { get; set; }
        protected IThreadChanger ThreadChanger { get; set; }

        protected IMainWindowVM MainWindowVM { get; set; }

        protected Window MainWindow { get; set; }

        public abstract void OnCreate();

        public void Start(Action onStop)
        {
            OnStop = onStop;

            InitDisposeManager = new InitDisposeManager();

            ThreadChanger = new ThreadChanger(InitDisposeManager);

            OnCreate();

            if (MainWindow == null)
            {
                MainWindow = new MainWindow();
            }

            MainWindow.DataContext = MainWindowVM;
            App.MainWindow = MainWindow;

            InitDisposeManager.Init();

            // var myResourceDictionary = new ResourceDictionary();
            // myResourceDictionary.Source = new Uri(@"/Strev.MoMock.Lib;View\UserControls\LightTheme.xaml", UriKind.RelativeOrAbsolute);
            // Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);

            MainWindow.Show();
        }

        public void Stop()
        {
            InitDisposeManager.Dispose();
            OnStop?.Invoke();
        }
    }
}