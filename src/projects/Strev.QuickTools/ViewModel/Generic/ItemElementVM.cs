using Strev.QuickTools.DomainModel.Enumeration;
using System.Windows.Input;

namespace Strev.QuickTools.ViewModel.Generic
{
    public class ItemElementVM : BaseVM, IItemElementVM
    {
        private readonly StackElementVM _parent;

        public ItemElementVM(StackElementVM parent, ElementType elementType)
        {
            _parent = parent;
            ElementType = elementType;
        }

        public ElementType ElementType { get; private set; }

        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public string Name
        {
            get
            {
                if (_parent != null)
                {
                    return _parent.Name;
                }
                return null;
            }
        }

        private int _height;

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }

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
                    _selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        private ICommand _command;

        public ICommand Command
        {
            get
            {
                return _command;
            }
            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged("Command");
                }
            }
        }

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged("IsReadOnly");
                }
            }
        }

        private TextSizeType _textSize;

        public TextSizeType TextSize
        {
            get
            {
                return _textSize;
            }
            set
            {
                if (_textSize != value)
                {
                    _textSize = value;
                    OnPropertyChanged("TextSize");
                }
            }
        }
    }
}