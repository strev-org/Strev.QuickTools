using Strev.QuickTools.Core.Service;
using Strev.QuickTools.Core.ViewModel;
using Strev.QuickTools.DomainModel.Enumeration;
using Strev.QuickTools.ViewModel.Generic;

namespace Strev.QuickTools.ViewModel
{
    public abstract class BaseScreenGenericVM : BaseScreenVM
    {
        protected BaseScreenGenericVM(IInitDisposeManager initDisposeManager, IScreenContainerVM parent)
            : base(initDisposeManager, parent)
        {
            LayoutVM = new LayoutVM(this);
        }

        public LayoutVM LayoutVM { get; private set; }

        public override void Init()
        {
            base.Init();
            InitLayout();
        }

        public abstract void InitLayout();

        public override ScreenType ScreenType => ScreenType.Generic;
    }
}