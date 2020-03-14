using System.Windows.Input;

namespace Strev.QuickTools.ViewModel.Generic
{
    public class ChoiceElementVM : BaseVM
    {
        private ChoiceVM _parent;
        private string _text;
        private bool _isCurrent;
        private object _id;

        public ChoiceElementVM(ChoiceVM parent, string text, object id)
        {
            _parent = parent;
            _text = text;
            _id = id;
        }

        public object Id => _id;

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
                    Notify();
                }
            }
        }

        public bool IsCurrent
        {
            get
            {
                return _isCurrent;
            }
            set
            {
                if (_isCurrent != value)
                {
                    _isCurrent = value;
                    Notify();
                }
            }
        }

        public void OnCurrent()
        {
        }

        #region SetCurrentCommand

        private RelayCommand _setCurrentCommand;

        public ICommand SetCurrentCommand
        {
            get
            {
                if (_setCurrentCommand == null)
                {
                    _setCurrentCommand = new RelayCommand((arg) => SetCurrent());
                }
                return _setCurrentCommand;
            }
        }

        #endregion SetCurrentCommand

        public void SetCurrent()
        {
            _parent.SetCurrent(this);
        }

        public void Notify()
        {
            OnPropertyChanged("Text");
            OnPropertyChanged("IsCurrent");
        }
    }
}