using System.Collections.ObjectModel;

namespace Strev.QuickTools.Core.ViewModel
{
    public interface IScreenContainerVM
    {
        ObservableCollection<IScreenVM> ScreenVMs { get; }
        string CurrentScreenName { get; }
        IScreenVM CurrentScreen { get; }

        void SelectScreen(IScreenVM screenVM);

        T GetScreen<T>() where T : class, IScreenVM;

        IWaitingManagerVM WaitingManagerVM { get; }
        IScreenContainerVM RootScreen { get; }
    }
}