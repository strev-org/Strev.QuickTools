using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Core.ViewModel;
using Strev.QuickTools.DomainModel.Enumeration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Strev.QuickTools.ViewModel
{
    public abstract class BaseScreenContainerVM : BaseScreenVM, IScreenContainerVM
    {
        protected BaseScreenContainerVM(IInitDisposeManager initDisposeManager, IScreenContainerVM parent)
            : base(initDisposeManager, parent)
        {
        }

        private readonly Dictionary<string, IScreenVM> _screenVmByName = new Dictionary<string, IScreenVM>();
        private readonly ObservableCollection<IScreenVM> _screenVMs = new ObservableCollection<IScreenVM>();

        public ObservableCollection<IScreenVM> ScreenVMs => _screenVMs;

        private bool _initialized = false;

        public override void Init()
        {
            base.Init();
            if (!_initialized)
            {
                foreach (var screenVM in ScreenVMs)
                {
                    screenVM.Init();
                }
                _initialized = true;
            }
        }

        public void ClearDisposeScreenVMs()
        {
            if (_initialized)
            {
                foreach (var screenVM in ScreenVMs)
                {
                    screenVM.Dispose();
                }
            }
            ScreenVMs.Clear();
        }

        public string CurrentScreenName
        {
            get
            {
                var currentScreen = CurrentScreen;
                if (currentScreen != null)
                {
                    return currentScreen.ScreenName;
                }
                return string.Empty;
            }
        }

        private IScreenVM _currentScreen;

        public IScreenVM CurrentScreen
        {
            get
            {
                return _currentScreen;
            }
            private set
            {
                if (_currentScreen != value)
                {
                    _currentScreen = value;
                    OnPropertyChanged("CurrentScreen");
                    OnPropertyChanged("CurrentScreenName");
                    OnPropertyChanged("CurrentScreenType");
                }
            }
        }

        public ScreenType CurrentScreenType
        {
            get
            {
                var currentScreen = CurrentScreen;
                if (currentScreen != null)
                {
                    return currentScreen.ScreenType;
                }
                return ScreenType.None;
            }
        }

        public T GetScreen<T>() where T : class, IScreenVM
        {
            return ScreenVMs.OfType<T>().FirstOrDefault();
        }

        public void SelectScreen(IScreenVM screenVM)
        {
            var currentScreen = CurrentScreen;
            if (currentScreen != null)
            {
                currentScreen.Selected = false;
            }
            if (screenVM == null)
            {
                return;
            }
            CurrentScreen = screenVM;
            CurrentScreen.Selected = true;
        }

        protected void AddScreenVM(IScreenVM screenVM)
        {
            _screenVMs.Add(screenVM);
            _screenVmByName[screenVM.ScreenName] = screenVM;
            if (_initialized)
            {
                screenVM.Init();
            }
        }

        ~BaseScreenContainerVM()
        {
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var currentScreen = CurrentScreen;
                currentScreen?.OnLeaveScreen();
                ClearDisposeScreenVMs();
            }
            base.Dispose(disposing);
        }

        public override void OnEnterScreen()
        {
            base.OnEnterScreen();
            if (CurrentScreen != null)
            {
                CurrentScreen.Selected = true;
            }
        }

        public override void OnLeaveScreen()
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Selected = false;
            }
            base.OnLeaveScreen();
        }

        public override ScreenType ScreenType => ScreenType.Container;
    }
}