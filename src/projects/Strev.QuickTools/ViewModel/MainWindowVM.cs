using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Core.ViewModel;
using System;

namespace Strev.QuickTools.ViewModel
{
    public class MainWindowVM : BaseMainWindowVM
    {
        private readonly Func<IScreenContainerVM, IScreenVM>[] _screenVmFactories;

        public MainWindowVM(IInitDisposeManager initDisposeManager, string name, params Func<IScreenContainerVM, IScreenVM>[] screenVmFactories) : base(initDisposeManager, null)
        {
            ScreenName = name;
            _screenVmFactories = screenVmFactories;

            initDisposeManager.AddInitializableDisposable(this);
        }

        public override void Init()
        {
            base.Init();

            IScreenVM first = null;
            foreach (var screenVmFactory in _screenVmFactories)
            {
                var screenVm = screenVmFactory(this);
                if (first == null)
                {
                    first = screenVm;
                }
                AddScreenVM(screenVm);
            }
            SelectScreen(first);
        }
    }
}