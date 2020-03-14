using Strev.QuickTools.Core.ViewModel;
using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Strev.QuickTools.ViewModel.Generic
{
    public class LayoutVM : BaseVM, ILayoutVM, IStackElementParentVM
    {
        private readonly IScreenVM _parent;
        private readonly Dictionary<string, StackElementVM> _elements = new Dictionary<string, StackElementVM>();
        private readonly Dictionary<string, List<Action<string, string>>> _onElementChanges = new Dictionary<string, List<Action<string, string>>>();

        public LayoutVM(IScreenVM parent)
        {
            _parent = parent;
            TopStackVM = new StackVM(this);
            BottomStackVM = new StackVM(this);
        }

        public void RegisterElementVM(StackElementVM elementVM)
        {
            if (elementVM.Name != null)
            {
                _elements[elementVM.Name] = elementVM;
            }
            if (elementVM.Element != null)
            {
                elementVM.Element.PropertyChanged += ElementVM_PropertyChanged;
            }
        }

        public ILayoutVM OnChange(string name, Action<string, string> action)
        {
            if (!_onElementChanges.ContainsKey(name))
            {
                _onElementChanges[name] = new List<Action<string, string>>();
            }
            _onElementChanges[name].Add(action);
            return this;
        }

        private void ElementVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var itemElementVM = sender as ItemElementVM;
            if (itemElementVM != null)
            {
                var name = itemElementVM.Name;
                var text = itemElementVM.Text;
                if (name != null)
                {
                    if (_onElementChanges.ContainsKey(name))
                    {
                        foreach (var action in _onElementChanges[name])
                        {
                            action(name, text);
                        }
                    }
                }
            }
        }

        public string GetValue(string name)
        {
            if (_elements.ContainsKey(name))
            {
                var elementVM = _elements[name];
                if (elementVM.Element != null)
                {
                    return elementVM.Element.Text;
                }
            }
            return null;
        }

        public void SetValue(string name, string value)
        {
            if (_elements.ContainsKey(name))
            {
                var elementVM = _elements[name];
                if (elementVM.Element != null)
                {
                    elementVM.Element.Text = value;
                }
            }
        }

        public int MarginSize { get; private set; }

        private StackVM _topStackVM;

        public StackVM TopStackVM
        {
            get
            {
                return _topStackVM;
            }
            set
            {
                if (_topStackVM != value)
                {
                    _topStackVM = value;
                    OnPropertyChanged("TopStackVM");
                }
            }
        }

        private StackVM _bottomStackVM;

        public StackVM BottomStackVM
        {
            get
            {
                return _bottomStackVM;
            }
            set
            {
                if (_bottomStackVM != value)
                {
                    _bottomStackVM = value;
                    OnPropertyChanged("BottomStackVM");
                }
            }
        }

        private StackElementVM _stackElementVM;

        public StackElementVM MiddleElementVM
        {
            get
            {
                return _stackElementVM;
            }
            set
            {
                if (_stackElementVM != value)
                {
                    _stackElementVM = value;
                    OnPropertyChanged("MiddleElementVM");
                }
            }
        }

        LayoutVM IStackElementParentVM.LayoutVM => this;

        public IStackVM TopStack()
        {
            return TopStackVM;
        }

        public IStackVM BottomStack()
        {
            return BottomStackVM;
        }

        public ILayoutVM StartLayout(int marginSize)
        {
            MarginSize = marginSize;
            return this;
        }

        public LayoutVM EndLayout()
        {
            return this;
        }

        public ILayoutVM AddTextZone(string name, string marginText, string text)
        {
            MiddleElementVM = StackElementVM.Create(this, null, name, ElementType.TextZone, marginText, text, TextSizeType.Tiny, null, true);
            return this;
        }
    }
}