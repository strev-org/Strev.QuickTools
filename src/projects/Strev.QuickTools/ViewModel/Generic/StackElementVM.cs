using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Collections.ObjectModel;

namespace Strev.QuickTools.ViewModel.Generic
{
    public class StackElementVM : BaseVM, IStackElementVM
    {
        private IStackElementParentVM _parent;

        public StackElementVM(IStackElementParentVM parent, string name)
        {
            _parent = parent;
            Name = name;
            // _parent.LayoutVM.RegisterElementVM(this);
        }

        public string Name { get; private set; }

        public int MarginSize
        {
            get
            {
                if (MarginElement == null)
                {
                    return 0;
                }
                return _parent.MarginSize;
            }
        }

        private ItemElementVM _marginElement;

        public ItemElementVM MarginElement
        {
            get
            {
                return _marginElement;
            }
            set
            {
                if (_marginElement != value)
                {
                    _marginElement = value;
                    OnPropertyChanged("MarginElement");
                }
            }
        }

        private IItemElementVM _element;

        public IItemElementVM Element
        {
            get
            {
                return _element;
            }
            set
            {
                if (_element != value)
                {
                    _element = value;
                    OnPropertyChanged("Element");
                    if (_parent != null && _parent.LayoutVM != null)
                    {
                        _parent.LayoutVM.RegisterElementVM(this);
                    }
                }
            }
        }

        public ObservableCollection<IItemElementVM> AssociatedElements { get; private set; } = new ObservableCollection<IItemElementVM>();

        public static StackElementVM Create(IStackElementParentVM parent, ObservableCollection<IStackElementVM> stackElementVMs, string name, ElementType elementType, string marginText, string text, TextSizeType textSize, Action onClick, bool isReadOnly)
        {
            StackElementVM elementVM;
            if (stackElementVMs != null && stackElementVMs.Count > 0)
            {
                elementVM = new StackElementVM(parent, null);
                elementVM.Element = new ItemElementVM(elementVM, ElementType.None);
                stackElementVMs.Add(elementVM);
            }

            elementVM = new StackElementVM(parent, name);
            if (marginText != null)
            {
                elementVM.MarginElement = new ItemElementVM(elementVM, ElementType.Label) { Text = marginText, TextSize = textSize };
            }
            elementVM.Element = new ItemElementVM(elementVM, elementType) { Text = text, TextSize = textSize, IsReadOnly = isReadOnly };
            if (onClick != null)
            {
                elementVM.Element.Command = new RelayCommand((obj) => onClick());
            }
            return elementVM;
        }
    }
}