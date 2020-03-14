using Strev.QuickTools.DomainModel.Enumeration;
using System.ComponentModel;
using System.Windows.Input;

namespace Strev.QuickTools.ViewModel.Generic
{
    public interface IItemElementVM : INotifyPropertyChanged
    {
        ElementType ElementType { get; }

        string Text { get; set; }

        // string Name { get; }

        // int Height { get; set; }

        // bool Selected { get; set; }

        ICommand Command { get; set; }

        // TextSizeType TextSize { get; set; }
    }
}