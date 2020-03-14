using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Strev.QuickTools.ViewModel.Generic
{
    public class StackVM : BaseVM, IStackVM, IStackElementParentVM
    {
        private readonly LayoutVM _parent;

        public StackVM(LayoutVM parent)
        {
            _parent = parent;
            StackElementVMs = new ObservableCollection<IStackElementVM>();
        }

        public int MarginSize => _parent.MarginSize;

        public ObservableCollection<IStackElementVM> StackElementVMs { get; private set; }

        public LayoutVM LayoutVM => _parent;

        private string _lastAdded;

        //private StackElementVM Create(string name, ElementType elementType, string marginText, string text, TextSizeType textSize, Action onClick)
        //{
        //    StackElementVM elementVM;
        //    if (StackElementVMs.Count > 0)
        //    {
        //        elementVM = new StackElementVM(this, null);
        //        elementVM.Element = new ItemElementVM(elementVM, ElementType.None);
        //        StackElementVMs.Add(elementVM);
        //    }

        //    elementVM = new StackElementVM(this, name);
        //    if (marginText != null)
        //    {
        //        elementVM.MarginElement = new ItemElementVM(elementVM, ElementType.Label) { Text = marginText, TextSize = textSize };
        //    }
        //    elementVM.Element = new ItemElementVM(elementVM, elementType) { Text = text, TextSize = textSize };
        //    if (onClick != null)
        //    {
        //        elementVM.Element.Command = new RelayCommand((obj) => onClick());
        //    }
        //    return elementVM;
        //}

        private StackVM AddItem(string name, ElementType elementType, string marginText, string text, TextSizeType textSize, Action onClick, bool isReadOnly)
        {
            StackElementVMs.Add(StackElementVM.Create(this, StackElementVMs, name, elementType, marginText, text, textSize, onClick, isReadOnly));
            _lastAdded = name;
            return this;
        }

        private StackVM AddItem(string name, ElementType elementType, string marginText, string text, TextSizeType textSize, Action onClick)
        {
            return AddItem(name, elementType, marginText, text, textSize, onClick, false);
        }

        public IStackVM AddInput(string name, string marginText, string text)
        {
            return AddItem(name, ElementType.Input, marginText, text, TextSizeType.Medium, null);
        }

        public IStackVM AddInput(string name, string marginText, string text, TextSizeType textSize)
        {
            return AddItem(name, ElementType.Input, marginText, text, textSize, null);
        }

        public IStackVM AddInputReadOnly(string name, string marginText, string text, TextSizeType textSize)
        {
            return AddItem(name, ElementType.Input, marginText, text, textSize, null, true);
        }

        public IStackVM AddLabel(string name, string marginText, string text)
        {
            return AddItem(name, ElementType.Label, marginText, text, TextSizeType.Medium, null);
        }

        public IStackVM AddLabel(string name, string marginText, string text, TextSizeType textSize)
        {
            return AddItem(name, ElementType.Label, marginText, text, textSize, null);
        }

        public IStackVM AddCenteredLabel(string name, string marginText, string text, TextSizeType textSize)
        {
            return AddItem(name, ElementType.CenteredLabel, marginText, text, textSize, null);
        }

        public IStackVM AddButton(string name, string text, Action onClick)
        {
            return AddItem(name, ElementType.Button, null, text, TextSizeType.Medium, onClick);
        }

        public IStackVM AddButton(string name, string text, TextSizeType textSize, Action onClick)
        {
            return AddItem(name, ElementType.Button, null, text, textSize, onClick);
        }

        public IStackVM AddChoice(Action<ChoiceVM> onPopulate, Action<ChoiceElementVM> onNewCurrent)
        {
            var choiceVM = new ChoiceVM(onNewCurrent);
            StackElementVMs.Add(choiceVM);
            onPopulate(choiceVM);
            return this;
        }

        public IStackVM OnChange(Action<string, string> action)
        {
            if (_lastAdded != null)
            {
                _parent.OnChange(_lastAdded, action);
            }
            return this;
        }

        public IStackVM WithButton(string text, Action onClick)
        {
            var lastItem = StackElementVMs.LastOrDefault() as StackElementVM;
            if (lastItem != null)
            {
                var elementVM = new ItemElementVM(lastItem, ElementType.Button) { Text = text, TextSize = TextSizeType.Medium, IsReadOnly = false };
                elementVM.Command = new RelayCommand((obj) => onClick());
                lastItem.AssociatedElements.Add(elementVM);
            }
            return this;
        }

        public ILayoutVM EndStack()
        {
            return _parent;
        }
    }
}