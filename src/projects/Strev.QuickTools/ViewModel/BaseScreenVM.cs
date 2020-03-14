using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Core.ViewModel;
using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Windows.Input;

namespace Strev.QuickTools.ViewModel
{
    public abstract class BaseScreenVM : BaseVM, IScreenVM
    {
        private readonly IScreenContainerVM _parent;

        public virtual IWaitingManagerVM WaitingManagerVM => RootScreen.WaitingManagerVM;

        public virtual IScreenContainerVM RootScreen => _parent?.RootScreen;

        protected IInitDisposeManager InitDisposeManager { get; }

        protected BaseScreenVM(IInitDisposeManager initDisposeManager, IScreenContainerVM parent)
        {
            InitDisposeManager = initDisposeManager;
            _parent = parent;
        }

        public IScreenContainerVM Parent => _parent;

        private string _screenName;

        public string ScreenName
        {
            get => _screenName;
            set
            {
                if (_screenName != value)
                {
                    _screenName = value;
                    OnPropertyChanged(nameof(ScreenName));
                }
            }
        }

        public abstract ScreenType ScreenType { get; }

        private bool _selected;

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    bool wasSelected = _selected;
                    _selected = value;
                    if (_selected && !wasSelected)
                    {
                        OnEnterScreen();
                    }
                    if (!_selected && wasSelected)
                    {
                        OnLeaveScreen();
                    }
                    OnPropertyChanged("Selected");
                }
            }
        }

        private bool _isVisible = true;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }

        private RelayCommand _selectScreenCommand;

        public ICommand SelectScreenCommand
        {
            get
            {
                if (_selectScreenCommand == null)
                {
                    _selectScreenCommand = new RelayCommand((arg) => SelectScreen());
                }
                return _selectScreenCommand;
            }
        }

        public void SelectScreen()
        {
            Parent?.SelectScreen(this);
        }

        public virtual void Init()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseScreenVM()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public virtual void OnEnterScreen()
        {
        }

        public virtual void OnLeaveScreen()
        {
        }
    }
}