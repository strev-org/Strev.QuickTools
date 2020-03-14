using Strev.QuickTools.DomainModel.Enumeration;
using System.ComponentModel;
using System.Windows.Input;

namespace Strev.QuickTools.Core.ViewModel
{
    public interface IScreenVM : INotifyPropertyChanged, IInitializable
    {
        IScreenContainerVM Parent { get; }

        IScreenContainerVM RootScreen { get; }
        string ScreenName { get; }
        ScreenType ScreenType { get; }
        bool Selected { get; set; }
        bool IsVisible { get; set; }
        ICommand SelectScreenCommand { get; }

        void OnEnterScreen();

        void OnLeaveScreen();
    }
}